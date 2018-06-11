using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using static Android.App.DatePickerDialog;
using static Android.App.TimePickerDialog;
using Xamarin.Facebook;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using PartyOrganizer.Core.Auth;
using Firebase.Xamarin.Auth;
using Android.Content;
using System.Linq;

namespace PartyOrganizer
{
    [Activity(Label = "Add Party", MainLauncher = false)]
    public class AddPartyActivity : Activity, IOnDateSetListener, IOnTimeSetListener
    {
        private IPartyRepositoryAsync _partyRepository;
        private FirebaseAuthLink _authLink;

        private List<PartyItem> _productList;
        private DateTime _partyTime;
        private ProductsListAdapter _dataAdapter;
        private EditText _newPartyNameEditText;
        private EditText _newPartyDescriptionEditText;
        private EditText _newPartyLocationEditText;
        private Button _newPartyAddButton;
        private Button _newPartyAddProductButton;
        private Button _newPartySetDateTimeButton;
        private TextView _newPartyDateTimeTextView;
        private EditText _newPartyProductNameEditText;
        private EditText _newPartyProductAmountEditText;
        private ListView _newPartyProductListView;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPartyView);

            FindViews();
            HandleEvents();

            _partyTime = DateTime.UtcNow;
            _newPartyDateTimeTextView.Text = _partyTime.ToString();

            _authLink = await FirebaseAuthLinkWrapper.GetAuthLink(FirebaseAuthType.Facebook, AccessToken.CurrentAccessToken.Token);
            _partyRepository = new PersistantPartyRepository(_authLink);

            _productList = new List<PartyItem>();
            _dataAdapter = new ProductsListAdapter(this, _productList);

            _newPartyProductListView.Adapter = _dataAdapter;
        }

        private void FindViews()
        {
            _newPartyNameEditText = FindViewById<EditText>(Resource.Id.newPartyNameEditText);
            _newPartyDescriptionEditText = FindViewById<EditText>(Resource.Id.newPartyDescriptionEditText);
            _newPartyLocationEditText = FindViewById<EditText>(Resource.Id.newPartyLocationEditText);
            _newPartyAddButton = FindViewById<Button>(Resource.Id.newPartyAddButton);
            _newPartyAddProductButton = FindViewById<Button>(Resource.Id.newPartyAddProductButton);
            _newPartyProductNameEditText = FindViewById<EditText>(Resource.Id.newPartyProductNameEditText);
            _newPartyProductAmountEditText = FindViewById<EditText>(Resource.Id.newPartyProductAmountEditText);
            _newPartyProductListView = FindViewById<ListView>(Resource.Id.newPartyProductsListView);
            _newPartySetDateTimeButton = FindViewById<Button>(Resource.Id.newPartySetDateTimeButton);
            _newPartyDateTimeTextView = FindViewById<TextView>(Resource.Id.newPartyDateTimeTextView);
        }

        private void HandleEvents()
        {
            _newPartyAddButton.Click += async (s, e) =>
            {
                try
                {   
                    var location = new LocationData()
                    {
                        Name = ValidateTextView(_newPartyLocationEditText)
                    };

                    var unixTimestamp = (Int32)(_partyTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    // Create model wrapper that'll provide proper validation
                    var partyContent = new PartyContent()
                    {
                        Description = ValidateTextView(_newPartyDescriptionEditText),
                        Image = _authLink?.User?.PhotoUrl,
                        Items = _productList?.ToDictionary(k => Guid.NewGuid().ToString(), v => v),
                        Location = location,
                        Name = ValidateTextView(_newPartyNameEditText),
                        Unix = unixTimestamp,
                        Order = Enumerable.Range(0, _productList.Count)
                    };

                    var party = new Party()
                    {
                        Content = partyContent
                    };

                    var newPartyId = await _partyRepository.Add(party);

                    if (newPartyId != null)
                    {
                        ShowDialog("Party's been added successfully", (sx, ex) =>
                        {
                            var intent = new Intent();
                            intent.SetClass(this, typeof(MenuActivity));
                            Finish();
                            StartActivity(intent);
                        });
                    }

                }
                catch (Exception ex)
                {
                    ShowDialog("Error occurred");
                    Console.WriteLine(ex.Message);
                }

            };

            _newPartyAddProductButton.Click += (s, e) =>
            {
                try
                {
                    var productName = _newPartyProductNameEditText.Text;
                    if (String.IsNullOrWhiteSpace(productName) || productName.Length < 3)
                    {
                        throw new FormatException();
                    }
                    var productAmount = Int32.Parse(_newPartyProductAmountEditText.Text);
                    if (productAmount < 1)
                        throw new InvalidCastException();

                    var product = new PartyItem { Amount = productAmount, Name = productName };
                    _productList.Add(product);
                    _dataAdapter.NotifyDataSetChanged();
                }
                catch (Exception ex)
                {
                    if (ex is FormatException)
                    {
                        ShowDialog("Product name should be min 3 letters long");
                    }
                    else
                        if (ex is InvalidCastException)
                    {
                        ShowDialog("Product amount should be greater than 0");
                    }
                    else
                    {
                        ShowDialog("Error occurred");
                    }
                    Console.WriteLine(ex.Message);
                }
            };

            _newPartySetDateTimeButton.Click += (s, e) =>
            {
                var year = DateTime.UtcNow.Year;
                var month = DateTime.UtcNow.Month;
                var day = DateTime.UtcNow.Day;

                var date = new DatePickerDialog(this, this, year, month, day);
                date.Show();
            };
        }

        private void ShowDialog(string message, EventHandler<DialogClickEventArgs> action = null)
        {
            var alert = new AlertDialog.Builder(this);

            alert.SetTitle("Add Party");
            alert.SetMessage(message);
            alert.SetCancelable(false);
            alert.SetPositiveButton("Ok", action);
            alert.Show();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            _partyTime = new DateTime(year, month, dayOfMonth);

            var hourOfDay = DateTime.UtcNow.Hour;
            var minute = DateTime.UtcNow.Minute;

            var time = new TimePickerDialog(this, this, hourOfDay, minute, true);
            time.Show();
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            _partyTime = new DateTime(_partyTime.Year, _partyTime.Month, _partyTime.Day, hourOfDay, minute, 0);
            _newPartyDateTimeTextView.Text = _partyTime.ToString();
        }

        private string ValidateTextView(TextView textView)
        {
            if (!String.IsNullOrWhiteSpace(textView.Text) && textView.Text.Length > 2)
            {
                return textView.Text;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}