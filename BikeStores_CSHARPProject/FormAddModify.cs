using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeStores_CSHARPProject
{
    public partial class FormAddModify : Form
    {
        public FormAddModify()
        {
            InitializeComponent();
        }

        public bool addCustomer;
        public Customer customer;

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DisplayCustomer()
        {
            FirstNameBox.Text = customer.first_name;
            LastNameBox.Text = customer.last_name;
            EmailBox.Text = customer.email;
            streetBox.Text = customer.street;
            CityBox.Text = customer.city;
            ZipCodeBox.Text = customer.zip_code;
            StateBox.Text= customer.state;
        }
        private void Add_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addCustomer)
                {
                    customer = new Customer();
                    this.PutCustomerData(customer);
                    try
                    {
                        customer.customer_id = CustomerDB.AddCustomer(customer);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    Customer newCustomer = new Customer();
                    newCustomer.customer_id = customer.customer_id;
                    this.PutCustomerData(newCustomer);
                    try
                    {
                        if (!CustomerDB.UpdateCustomer(customer, newCustomer))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that customer.", "Database Error");
                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            customer = newCustomer;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }
        private bool IsValidData()
        {
            return
                Validator.IsPresent(FirstNameBox) &&
                Validator.IsPresent(LastNameBox) &&
                Validator.IsPresent(EmailBox) &&
                Validator.IsValidEmail(EmailBox) &&
                Validator.IsPresent(streetBox) &&
                Validator.IsPresent(CityBox) &&
                Validator.IsPresent(StateBox) &&
                Validator.IsPresent(ZipCodeBox)&&
                Validator.IsInt32(ZipCodeBox);

        }
        private void FormAddModify_Load(object sender, EventArgs e)
        {
            if (addCustomer)
            {
                //text will be add customer, combo box will not be selected
                this.Text = "Add Customer";
            }
            else
            {
                this.Text = "Modify Customer";
                this.DisplayCustomer();
                Add.Text = "Modify";
            }
        }
        private void PutCustomerData(Customer newCustomer)
        {
            newCustomer.first_name = FirstNameBox.Text;
            newCustomer.last_name = LastNameBox.Text;
            newCustomer.email = EmailBox.Text;
            newCustomer.street = streetBox.Text;
            newCustomer.city = CityBox.Text;
            newCustomer.state = StateBox.Text.ToUpper();
            newCustomer.zip_code = ZipCodeBox.Text;
        }
        private void FirstNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only Character Validation
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        private void LastNameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only Character Validation
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        private void CityBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only Character Validation
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        private void StateBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only Character Validation
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }
    }
}
