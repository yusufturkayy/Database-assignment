using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts.Model
{
    public class ContactDatabase
    {
   

        private SQLiteAsyncConnection? Database;

       

        private async Task Init()
        {
            try
            {
                if (Database is not null)
                    return;

                Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                var result = await Database.CreateTableAsync<ContactInfo>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
                throw;
            }
        }




       public async Task<List<ContactInfo>> GetAllContactsAsync()
        {
            await Init();
            try
            {
                var contacts = await Database.Table<ContactInfo>().ToListAsync();
                return contacts;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading contacts: {ex.Message}");
                throw;
            }
        }


        public async Task InsertContact(ContactInfo contact)
        {
            await Init();
            try
            {
                var result = await Database.InsertAsync(contact);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error inserting contact: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateContact(ContactInfo contact)
        {
            await Init();
            await Database.UpdateAsync(contact);
        }

        public async Task<ContactInfo> GetContactByIdAsync(int id)
        {
            await Init();
            ContactInfo contact = await Database.Table<ContactInfo>().Where(c => c.Id == id).FirstOrDefaultAsync();

            return contact;


        }

        public async Task DeleteContact(ContactInfo contact) 
        { 
            await Init(); 
            await Database.DeleteAsync(contact);
        }
    }
 }