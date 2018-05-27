using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using PartyOrganizer.Core.Model;
using PartyOrganizer.Core.Repository;
using PartyOrganizer.Core.Repository.Interfaces;
using System;

namespace PartyOrganizer
{
    [Activity(Label = "Add Party", MainLauncher = false)]
    public class AddPartyActivity : Activity
    {
        private IPartyRepository _partyRepository;

        private EditText _newPartyNameEditText;
        private EditText _newPartyDescriptionEditText;
        private EditText _newPartyLocationEditText;
        private Button _newPartyAddButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPartyView);

            _partyRepository = new PartyRepository();

            FindViews();

            HandleEvents();
        }

        private void FindViews()
        {
            _newPartyNameEditText = FindViewById<EditText>(Resource.Id.newPartyNameEditText);
            _newPartyDescriptionEditText = FindViewById<EditText>(Resource.Id.newPartyDescriptionEditText);
            _newPartyLocationEditText = FindViewById<EditText>(Resource.Id.newPartyLocationEditText);
            _newPartyAddButton = FindViewById<Button>(Resource.Id.newPartyAddButton);
        }

        private void HandleEvents()
        {
            _newPartyAddButton.Click += (object sender, EventArgs e) =>
            {
                // 1. Validate
                // 2. Add to the DB
                // 3. Show success/failure info
                // 4. Return to the previous activity

                PartyInfo party;

                try
                {
                    // Create model wrapper that'll provide proper validation
                    party = new PartyInfo()
                    {
                        Name = _newPartyNameEditText.Text,
                        Admin = null,
                        Description = _newPartyDescriptionEditText.Text,
                        Location = _newPartyLocationEditText.Text
                    };

                    _partyRepository.Add(party);
                }
                catch (Exception ex)
                {
                    party = null;
                    Console.WriteLine(ex.InnerException.Message);
                }

                if (party != null)
                {
                    var intent = new Intent();
                    intent.SetClass(this, typeof(PartiesActivity));

                    StartActivity(intent);
                }


            };
      
        }
    }
}