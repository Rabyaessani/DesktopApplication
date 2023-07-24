
namespace MintProject
{
    using System.Data;
    using System.Data.OleDb;
    using System.IO.Compression;

    public partial class Form1 : Form
    {
        
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\HP\Downloads\CustomerDB.accdb");
        private string currentSearchValue = string.Empty;
        public Form1()
        {
            InitializeComponent();


        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Enter_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchValue = txtSearch.Text.Trim();
                string query = "SELECT  id,name,street,city,state,zip,phone, email,notes,points FROM customer WHERE name LIKE @SearchValue OR street LIKE @SearchValue OR city LIKE @SearchValue OR state LIKE @SearchValue OR zip LIKE @SearchValue OR phone LIKE @SearchValue OR email LIKE @SearchValue OR notes LIKE @SearchValue ORDER BY id DESC";
                //string query = "SELECT id,name,street,city,zip,phone, email,notes,points FROM customer WHERE  @SearchValue = name OR %SearchValue% = street  OR %SearchValue% = city OR state = %SearchValue% OR zip = %SearchValue% OR phone = %SearchValue% OR email = %SearchValue% OR notes = @SearchValue";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("@SearchValue", "%" + SearchValue + "%");
                int rowCount = GetRowCount(SearchValue);
                DataLabel.Text = "Number of Data: " + rowCount.ToString();
                con.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                if (SearchValue != "")
                {
                    if (reader.Read())
                    {

                        IDTextbox.Text = reader["id"].ToString();
                        NameTextbox.Text = reader["name"].ToString();
                        StreetTextBox.Text = reader["street"].ToString();
                        CityTextBox.Text = reader["city"].ToString();
                        StateTextBox.Text = reader["state"].ToString();
                        ZipTextBox.Text = reader["zip"].ToString();
                        PhoneTextBox.Text = reader["phone"].ToString();
                        EmailTextBox.Text = reader["email"].ToString();
                        NotesTextBox.Text = reader["notes"].ToString();
                        pointsTextBox.Text = reader["points"].ToString();

                    }

                    else
                    {
                        IDTextbox.Text = string.Empty;
                        NameTextbox.Text = string.Empty;
                        StreetTextBox.Text = string.Empty;
                        CityTextBox.Text = string.Empty;
                        StateTextBox.Text = string.Empty;
                        ZipTextBox.Text = string.Empty;
                        PhoneTextBox.Text = string.Empty;
                        EmailTextBox.Text = string.Empty;
                        NotesTextBox.Text = string.Empty;
                        pointsTextBox.Text = string.Empty;
                        MessageBox.Show("Data not found");
                        //txtSearch.Text = string.Empty;

                    }
                    con.Close();

                }
            }

            catch (Exception)
            {
                MessageBox.Show("An error occurred");
            }


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string newSearchValue = txtSearch.Text.Trim();
            if (newSearchValue != currentSearchValue)
            {
                currentSearchValue = newSearchValue + currentSearchValue;
                PerformDynamicSearch(newSearchValue);
                //     MessageBox.Show(newSearchValue);
            }


        }
        private void PerformDynamicSearch(string SearchValue)
        {

            try
            {

                if (SearchValue != "")
                {
                    string query = "SELECT  id,name,street,city,state,zip,phone, email,notes,points FROM customer WHERE name LIKE @SearchValue OR street LIKE @SearchValue OR city LIKE @SearchValue OR state LIKE @SearchValue OR zip LIKE @SearchValue OR phone LIKE @SearchValue OR email LIKE @SearchValue OR notes LIKE @SearchValue ORDER BY id DESC";
                    //string query = "SELECT id,name,street,city,zip,phone, email,notes,points FROM customer WHERE  @SearchValue = name OR %SearchValue% = street  OR %SearchValue% = city OR state = %SearchValue% OR zip = %SearchValue% OR phone = %SearchValue% OR email = %SearchValue% OR notes = @SearchValue";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    cmd.Parameters.AddWithValue("@SearchValue", "%" + SearchValue + "%");
                    int rowCount = GetRowCount(SearchValue);
                    DataLabel.Text = "Number of Data: " + rowCount.ToString();
                    con.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            IDTextbox.Text = reader["id"].ToString();
                            NameTextbox.Text = reader["name"].ToString();
                            StreetTextBox.Text = reader["street"].ToString();
                            CityTextBox.Text = reader["city"].ToString();
                            StateTextBox.Text = reader["state"].ToString();
                            ZipTextBox.Text = reader["zip"].ToString();
                            PhoneTextBox.Text = reader["phone"].ToString();
                            EmailTextBox.Text = reader["email"].ToString();
                            NotesTextBox.Text = reader["notes"].ToString();
                            pointsTextBox.Text = reader["points"].ToString();
                        }
                    }
                    else
                    {
                        ClearField();
                        con.Close();
                        //  MessageBox.Show("Data not found");
                    }

                }
                else
                {
                    ClearField();
                    con.Close();
                }
                con.Close();
                //txtSearch.Text = string.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error Occured:" + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private int GetRowCount(string SearchValue)
        {
            try
            {
                if (SearchValue == string.Empty)
                {
                    int RowCount = 0;
                    return RowCount;
                }
                else
                {
                    string query2 = "Select Count (*) FROM customer WHERE name LIKE @SearchValue OR street LIKE @SearchValue OR city LIKE @SearchValue OR state LIKE @SearchValue OR zip LIKE @SearchValue OR phone LIKE @SearchValue OR email LIKE @SearchValue OR notes LIKE @SearchValue ";
                    OleDbCommand cmd = new OleDbCommand(query2, con);
                    cmd.Parameters.AddWithValue("@SearchValue", "%" + SearchValue + "%");
                    con.Open();
                    int RowCount = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return RowCount;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error Occured:" + ex.Message);
                return 0;
            }

        }

        private void ClearField()
        {
            IDTextbox.Text = string.Empty;
            NameTextbox.Text = string.Empty;
            StreetTextBox.Text = string.Empty;
            CityTextBox.Text = string.Empty;
            StateTextBox.Text = string.Empty;
            ZipTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            NotesTextBox.Text = string.Empty;
            pointsTextBox.Text = string.Empty;
        }



    }

}
