using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diseño
{
    public partial class clientes : Form
    {

        SqlConnection con = new SqlConnection();
        String id;
        public clientes()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clientes_Load(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jenni\\source\\repos\\Diseño\\Database1.mdf;Integrated Security=True ";
                con.Open();
                //MessageBox.Show("Conexión realizada correctamente");

                String consulta = "SELECT * FROM clientes";
                DataTable dt= new DataTable();
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, con);
                adaptador.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (SqlException er)
            {
                MessageBox.Show("Error: " + er);
               
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jenni\\source\\repos\\Diseño\\Database1.mdf;Integrated Security=True ";
                con.Open();
                //MessageBox.Show("Conexión realizada correctamente");

                String consulta = "INSERT INTO clientes(Nombre, Celular, Correo, Direccion) VALUES('" + txtnombre.Text+"','"+txtcel.Text+"','"+txtcorreo.Text+"','"+txtdir.Text+"')";
                SqlCommand comando = new SqlCommand(consulta, con);
                comando.ExecuteNonQuery();
                MessageBox.Show("Cliente agregado correctamente");
                String consulta2 = "SELECT * FROM clientes";
                DataTable dt = new DataTable();
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta2, con);
                adaptador.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();



            }
            catch (SqlException er)
            {
                MessageBox.Show("Error: " + er);
                
            }
        }

        private void btnact_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jenni\\source\\repos\\Diseño\\Database1.mdf;Integrated Security=True ";
                con.Open();
                //MessageBox.Show("Conexión realizada correctamente");

                String consulta = "UPDATE Clientes SET Nombre='" + txtnombre.Text + "', Celular='" + txtcel.Text + "', Direccion='" + txtdir.Text + "', Correo='" + txtcorreo.Text + "' WHERE idCliente=" + id;


                SqlCommand comando = new SqlCommand(consulta, con);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha actualizado correctamente ");
                String consulta2 = "SELECT * FROM clientes";
                DataTable dt = new DataTable();
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta2, con);
                adaptador.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                txtnombre.Clear();
                txtcel.Clear();
                txtcorreo.Clear();
                txtdir.Clear();
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er);
                
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtnombre.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtcel.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtdir.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtcorreo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jenni\\source\\repos\\Diseño\\Database1.mdf;Integrated Security=True ";
                con.Open();
                //MessageBox.Show("Conexión realizada correctamente");

                String consulta = " DELETE FROM clientes WHERE IdCliente= " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                SqlCommand comando = new SqlCommand(consulta, con);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha eliminado correctamente :)");
             
                String consulta2 = "SELECT * FROM clientes";
                DataTable dt = new DataTable();
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta2, con);
                adaptador.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                //txtnombre.Clear();
                //txtcel.Clear();
                //txtcorreo.Clear();
                //txtdir.Clear();
            }
            catch (Exception er)
            {
                MessageBox.Show("Error " + er);

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jenni\\source\\repos\\Diseño\\Database1.mdf;Integrated Security=True ";
                con.Open();
                //MessageBox.Show("Conexión realizada correctamente");

                String consulta = "SELECT * FROM clientes WHERE Nombre LIKE '" + textBox5.Text + "%'";
                DataTable dt = new DataTable();
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, con);
                adaptador.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (SqlException er)
            {
                MessageBox.Show("Error: " + er);

            }
        }
    }
}
