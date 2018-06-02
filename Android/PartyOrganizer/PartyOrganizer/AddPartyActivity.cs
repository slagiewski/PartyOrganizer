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



namespace PartyOrganizer
{
    [Activity(Label = "Add Party", MainLauncher = false)]
    public class AddPartyActivity : Activity, IOnDateSetListener, IOnTimeSetListener
    {
        private IPartyRepositoryAsync _partyRepository;

        private Profile _profile;
        private List<PartyItem> _productList;
        private List<PartyMember> _partyMembersList;
        private List<PartyMember> _partyPendingList;
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

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPartyView);

            FindViews();
            HandleEvents();

            _profile = Profile.CurrentProfile;
            _partyTime = DateTime.UtcNow;
            _newPartyDateTimeTextView.Text = _partyTime.ToString();
            _partyRepository = new WebPartyRepository();
            _partyMembersList = new List<PartyMember>();
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
            _newPartyAddButton.Click += async (object sender, EventArgs e) =>
            {
                // 1. Validate
                // 2. Add to the DB
                // 3. Show success/failure info
                // 4. Return to the previous activity

                try
                {
                    var location = new LocationData()
                    {
                        Name = _newPartyLocationEditText.Text
                    };

                    var unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    // Create model wrapper that'll provide proper validation
                    var partyContent = new PartyContent()
                    {
                        Description = _newPartyDescriptionEditText.Text,
                        // Party admin image
                        // Image =
                        Items = _productList,
                        Location = location,
                        Name = _newPartyNameEditText.Text,
                        Unix = unixTimestamp
                    };

                    var party = new Party()
                    {
                        Content = partyContent,
                        Members = _partyMembersList,
                        Pending = null
                    };
                    await _partyRepository.Add(party);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }

                //if (party != null)
                //{
                //    var intent = new Intent();
                //    intent.SetClass(this, typeof(PartiesActivity));

                //    StartActivity(intent);
                //}
            };                

            _newPartyAddProductButton.Click += (object sender, EventArgs e) =>
            {
                var productName = _newPartyProductNameEditText.Text;
                var productAmount = Int32.Parse(_newPartyProductAmountEditText.Text);
                var product = new PartyItem(productAmount, productName);
                _productList.Add(product);
                _dataAdapter.NotifyDataSetChanged();
            };

            _newPartySetDateTimeButton.Click += (object sender, EventArgs e) =>
            {
                var year = DateTime.UtcNow.Year;
                var month = DateTime.UtcNow.Month;
                var day = DateTime.UtcNow.Day;

                var date = new DatePickerDialog(this, this, year, month, day);
                date.Show();
            };
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
    }
}