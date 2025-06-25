using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StationeryStore
{
    // Coding Start Signup From 
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        // Start joint design from to database 

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Desktop Application\StationeryStore\StationeryStore.mdf;Integrated Security=True;Connect Timeout=30");

        private void btnSignupNow_Click(object sender, EventArgs e)
        {
            try
            {
                // এই কোড গুলো হচ্ছে রেজিস্ট্রেশন ফর্মের টেক্স বক্সের নাম - এই বক্সগুলো থেকে ডাটা নিয়ে ডাটাবেজে সংরক্ষণ করবে।
                if (first_name.Text != "" && l_name.Text != "" && date.Text != "" && gender.Text != ""
                    && address.Text != "" && email.Text != "" && password.Text != "" && con_password.Text != "")
                {
                    if (password.Text == con_password.Text)
                    {
                        int v = check(email.Text);
                        if (v != 1)
                        {
                            connection.Open();
                            // এখানে ডাটাবেজে ডাটা সংরক্ষণের জন্য SQL কমান্ড তৈরি করা হচ্ছে। @f_name,@l_last এই নাম গুলো হচ্ছে 
                            // রেজিস্ট্রেশন টেবিলের কলামের নাম এবং পরের অংশে রেজিস্ট্রেশন ফর্মের টেক্স বক্সের নাম যুক্ত করা হয়েছে।

                            SqlCommand command = new SqlCommand("INSERT INTO RegistrationTbl values (@f_name,@l_last," +
                             " @b_date,@gender,@address,@email,@password)", connection);

                            // এখানে প্যারামিটার যুক্ত করা হচ্ছেঃ ডাবল কোটেশনের ভিতরে হচ্ছে ডাটাবেজের টেবিলের ভেলোর নাম
                            // যেমনঃ("@f_name") এবং পরের অংশে রেজিস্ট্রেশন ফর্মের টেক্স বক্সের নাম যুক্ত করা হয়েছে (first_name.Text)।

                            command.Parameters.AddWithValue("@f_name", first_name.Text);
                            command.Parameters.AddWithValue("@l_last", l_name.Text);
                            command.Parameters.AddWithValue("@b_date", Convert.ToDateTime(date.Text));
                            command.Parameters.AddWithValue("@gender", gender.Text);
                            command.Parameters.AddWithValue("@address", address.Text);
                            command.Parameters.AddWithValue("@email", email.Text);
                            command.Parameters.AddWithValue("@password", password.Text);

                            command.ExecuteNonQuery(); // Execute the command to insert data into the database
                            connection.Close(); // Close the database connection
                            MessageBox.Show("Registration Successful!"); // Show a success message


                            first_name.Text = ""; // Clear the first name text box
                            l_name.Text = ""; // Clear the last name text box
                            date.Text = ""; // Clear the date text box  
                            gender.Text = "";
                            address.Text = ""; // Clear the address text box    
                            email.Text = ""; // Clear the email text box
                            password.Text = ""; // Clear the password text box
                            con_password.Text = ""; // Clear the confirm password text box

                        }
                        else
                        {
                            MessageBox.Show("You are already Registerted! Please use a different name and password."); // Show an error message if email exists
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password and Confirm Password do not match!"); // Show an error message if passwords do not match
                    }
                }
                else
                {
                    MessageBox.Show("Please fill all the fields!"); // Show an error message if any field is empty
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Show any exception message that occurs during the process
            }
        }

        int check(string email) // This method checks if the email already exists in the database
        {
            connection.Open();
            // Check if the email already exists in the database    
            string query = "SELECT COUNT(*) FROM RegistrationTbl WHERE Email = '" + email + "'";

            SqlCommand command = new SqlCommand(query, connection);

            int v = (int)command.ExecuteScalar();
            connection.Close();
            return v; // Return the count of records with the given email

        }

        // end database joint


        // Start Pssword showing and end

        private void check_password_CheckedChanged(object sender, EventArgs e)
        {
            if (check_password.Checked) //
            {
                password.UseSystemPasswordChar = false;
                con_password.UseSystemPasswordChar = false;
            }
            else
            {
                password.UseSystemPasswordChar = true;
                con_password.UseSystemPasswordChar = true;
            }
        }
        // End Pssword showing and end
        // Coding End Signup From



        private void panelSignup_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void lblAlreadyCreateAccount_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide the current RegistrationForm when the panel is painted
            Login loginForm = new Login(); // Create an instance of the Login form
            loginForm.Show(); // Show the Login form
        }

        private void lblExitSignup_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Close the application when the exit label is clicked
        }
    }
}