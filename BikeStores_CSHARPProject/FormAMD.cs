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
    public partial class FormAMD : Form
    {
        public FormAMD()
        {
            InitializeComponent();
        }

        private Customer customer;

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DisplayCustomer()
        {
            FirstNameBox.Text = customer.first_name;
            LastNameBox.Text = customer.last_name;
            //PhoneBox.Text = customer.phone;
            EmailBox.Text = customer.email;
            streetBox.Text = customer.street;
            CityBox.Text = customer.city;
            StateBox.Text = customer.state;
            ZipCodeBox.Text = customer.zip_code;
            Add.Enabled = true;
            Modify.Enabled = true;
            Delete.Enabled = true;
        }
        private void Add_Click(object sender, EventArgs e)
        {
            FormAddModify addCustomerForm = new FormAddModify();
            addCustomerForm.addCustomer = true;
            DialogResult result = addCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                customer = addCustomerForm.customer;
                CustIDBox.Text = customer.customer_id.ToString();
                this.DisplayCustomer();
            }
        }
        private void GetCustomer(int customerID)
        {
            try
            {
                customer = CustomerDB.GetCustomer(customerID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void ClearControls()
        {
            CustIDBox.Text = "";
            FirstNameBox.Text = "";
            LastNameBox.Text = "";
            //PhoneBox.Text = "";
            EmailBox.Text = "";
            streetBox.Text = "";
            CityBox.Text = "";
            StateBox.Text = "";
            ZipCodeBox.Text = "";
            Add.Enabled = true;
            Modify.Enabled = false;
            Delete.Enabled = false;
            CustIDBox.Select();
        }
        private void Modify_Click(object sender, EventArgs e)
        {
            FormAddModify modifyCustomerForm = new FormAddModify();
            modifyCustomerForm.addCustomer = false;
            modifyCustomerForm.customer = customer;
            DialogResult result = modifyCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                customer = modifyCustomerForm.customer;
                this.DisplayCustomer();
            }
            else if (result == DialogResult.Retry)
            {
                this.GetCustomer(customer.customer_id);
                if (customer != null)
                    this.DisplayCustomer();
                else
                    this.ClearControls();
            }

        }

        private void Load_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(CustIDBox) &&
                Validator.IsInt32(CustIDBox))
            {
                int customerID = Convert.ToInt32(CustIDBox.Text);
                this.GetCustomer(customerID);
                if (customer == null)
                {
                    MessageBox.Show("No customer found with this ID. " +
                         "Please try again.", "Customer Not Found");
                    this.ClearControls();
                }
                else
                    this.DisplayCustomer();
                    
            }

        }

        private void FormAMD_Load(object sender, EventArgs e)
        {
            Modify.Enabled = true;
            Delete.Enabled = false;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Delete " + customer.first_name + "?",
               "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!CustomerDB.DeleteCustomer(customer))
                    {
                        MessageBox.Show("Another user has updated or deleted " +
                            "that customer.", "Database Error");
                        this.GetCustomer(customer.customer_id);
                        if (customer != null)
                            this.DisplayCustomer();
                        else
                            this.ClearControls();
                    }
                    else
                        this.ClearControls();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }        
    }
}