using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using PartyOrganizer.Adapters;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Model.Party;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace PartyOrganizer
{
    [Activity(Label = "Add Party", MainLauncher = false)]
    public class AddPartyActivity : Activity
    {
        private IPartyRepositoryAsync _partyRepository;

        private List<PartyItem> _productList;
        private List<PartyMember> _partyMembersList;
        private List<PartyMember> _partyPendingList;
        private ProductsListAdapter _dataAdapter;
        private EditText _newPartyNameEditText;
        private EditText _newPartyDescriptionEditText;
        private EditText _newPartyLocationEditText;
        private Button _newPartyAddButton;
        private Button _newPartyAddProductButton;
        private EditText _newPartyProductNameEditText;
        private EditText _newPartyProductAmountEditText;
        private ListView _newPartyProductListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPartyView);
            FindViews();

            HandleEvents();
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
        }

        private void HandleEvents()
        {
            _newPartyAddButton.Click += async (object sender, EventArgs e) =>
            {
                // 1. Validate
                // 2. Add to the DB
                // 3. Show success/failure info
                // 4. Return to the previous activity

                Party party;
                PartyContent partyContent;
                LocationData location;

                try
                {
                    location = new LocationData()
                    {
                        Name = _newPartyLocationEditText.Text
                    };

                    // Create model wrapper that'll provide proper validation
                    partyContent = new PartyContent()
                    {
                        Description = _newPartyDescriptionEditText.Text,
                        // Party admin image
                        // Image =
                        Items = _productList,
                        Location = location,
                        Name = _newPartyNameEditText.Text
                    };

                    party = new Party()
                    {
                        Content = partyContent,
                        Members = _partyMembersList,
                        Pending = _partyPendingList
                    };
                    await _partyRepository.Add(party);
                }
                catch (Exception ex)
                {
                    party = null;
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
      
        }
    }
}