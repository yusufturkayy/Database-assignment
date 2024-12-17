using Microsoft.Maui.ApplicationModel.Communication;
using MyContacts.Model;

namespace MyContacts.Views;

public partial class ContactsPage : ContentPage
{

    public ContactsPage()
    {
		InitializeComponent();
    }

    ContactsRepository contactsRepository = new ContactsRepository();
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var contacts = await contactsRepository.GetContacts();
            if (contacts != null)
            {
                ContactsList.ItemsSource = null; 
                ContactsList.ItemsSource = contacts; 
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load contacts: {ex.Message}", "OK");
        }
    }
    private void AddContactButton_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private void ContactsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedContact = e.SelectedItem as ContactInfo;
        if(selectedContact != null) {
            Shell.Current.GoToAsync(nameof(EditContactPage)+"?id="+selectedContact.Id);

        }
    }
}