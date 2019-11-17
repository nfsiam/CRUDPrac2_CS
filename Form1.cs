using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRUDPrac2
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\Edu\Programs\C#\Practice\CRUDPrac2\Database1.mdf;Integrated Security=True");
        int stdID;
        public Form1()
        {
            InitializeComponent();
            LoadFromDB();
        }

        private void LoadFromDB()
        {
            
            SqlCommand cmd = new SqlCommand("Select * from StudentsTb", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            if(isValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into studentsTb Values (@name, @FName, @Roll, @Address, @Mobile) ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", tbName.Text);
                cmd.Parameters.AddWithValue("@FName", tbFName.Text);
                cmd.Parameters.AddWithValue("@Roll", tbRoll.Text);
                cmd.Parameters.AddWithValue("@Address", tbAdd.Text);
                cmd.Parameters.AddWithValue("@Mobile", tbMob.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted successfully");

                LoadFromDB();

                ClearTBs();

            }
        }

        private bool isValid()
        {
            if(tbName.Text== string.Empty)
            {
                MessageBox.Show("Name is empty");
                return false;
            }
            else  if (tbFName.Text == string.Empty)
            {
                MessageBox.Show("Father's Name is empty");
                return false;
            }
            else if (tbAdd.Text == string.Empty)
            {
                MessageBox.Show("Address is empty");
                return false;
            }
            else if (tbMob.Text == string.Empty)
            {
                MessageBox.Show("Mobile is empty");
                return false;
            }
            else if (tbRoll.Text == string.Empty)
            {
                MessageBox.Show("Roll number is empty");
                return false;
            }
            else
                return true;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearTBs();
            stdID = 0;
        }

        private void ClearTBs()
        {
            tbAdd.Clear();
            tbFName.Clear();
            tbName.Clear();
            tbMob.Clear();
            tbRoll.Clear();
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            stdID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            tbName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            tbFName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            tbRoll.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            tbAdd.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            tbMob.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (stdID>0)
            {
                SqlCommand cmd = new SqlCommand("Update studentsTb set Name = @name, FatherName = @FName, RollNumber = @Roll, Address = @Address, Mobile = @Mobile where StudentId = @ID ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", tbName.Text);
                cmd.Parameters.AddWithValue("@FName", tbFName.Text);
                cmd.Parameters.AddWithValue("@Roll", tbRoll.Text);
                cmd.Parameters.AddWithValue("@Address", tbAdd.Text);
                cmd.Parameters.AddWithValue("@Mobile", tbMob.Text);
                cmd.Parameters.AddWithValue("@ID", stdID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated successfully");

                LoadFromDB(); 
            }
            else
            {
                MessageBox.Show("Select student row to update");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (stdID > 0)
            {
                SqlCommand cmd = new SqlCommand("Delete from studentsTb where StudentId = @ID ", con);
                cmd.CommandType = CommandType.Text;
               
                cmd.Parameters.AddWithValue("@ID", stdID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deleted successfully");

                LoadFromDB();
            }
            else
            {
                MessageBox.Show("Select student row to delete");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.StudentsTb' table. You can move, or remove it, as needed.
            this.studentsTbTableAdapter.Fill(this.database1DataSet.StudentsTb);

        }
    }
}
