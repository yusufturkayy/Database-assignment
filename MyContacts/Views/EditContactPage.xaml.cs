using MyContacts.Model;

namespace MyContacts.Views;

[QueryProperty(nameof(id), "id")]
public partial class EditContactPage : ContentPage
{
    private readonly ContactsRepository repository = new ContactsRepository();
    private ContactInfo? contact;
    public string? id { get; set; }
    
    public EditContactPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (id != null)
        {
            contact = await repository.GetContact(Int32.Parse(id));
            if (contact != null)
            {
                NameEntry.Text = contact.NameSurname;
                PhoneEntry.Text = contact.PhoneNumber;
                EmailEntry.Text = contact.Email;
            }
        }
    }

    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        // Update contact information
        contact.NameSurname = NameEntry.Text;
        contact.PhoneNumber = PhoneEntry.Text;
        contact.Email = EmailEntry.Text;

        // Add UpdateContact method to ContactsRepository
        await repository.UpdateContact(contact);
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Are you Sure?", "Are you sure to delete", "Yes", "No");
        if (answer)
        {
            await repository.DeleteContact(contact);
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}