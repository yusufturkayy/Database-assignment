using MyContacts.Model;

namespace MyContacts.Views;

public partial class AddContactPage : ContentPage
{
	public AddContactPage()
	{
		InitializeComponent();


    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");
    }

   private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        try 
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Error", "Name is required", "OK");
                return;
            }

            ContactInfo contact = new ContactInfo
            {
                NameSurname = NameEntry.Text,
                PhoneNumber = PhoneEntry.Text,
                Email = EmailEntry.Text
            };

            ContactsRepository repository = new ContactsRepository();
            await repository.AddContact(contact);
            await DisplayAlert("Success", "Contact added successfully", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save contact: {ex.Message}", "OK");
        }
    }
}