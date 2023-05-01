using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Diseño
{
    public partial class compra : Form
    {
        
        SqlConnection con = new SqlConnection();
        String Nombre, Precio, cantidad;
        private DataTable dt;
        double pre, sub, iva, total;
        int cant;
        public compra()
        {
            InitializeComponent();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void compra_Load(object sender, EventArgs e)
        {

            dt = new DataTable();
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Precio", typeof(int));
            dt.Columns.Add("Cantidad", typeof(int));
            dataGridView1.DataSource = dt;

        }

        //private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true; // Suprime la tecla Enter para evitar que mueva el foco a la siguiente celda.

        //        int currentCol = dataGridView1.CurrentCell.ColumnIndex;
        //        int currentRow = dataGridView1.CurrentCell.RowIndex;

        //        if (currentRow == dataGridView1.RowCount - 1)
        //        {
        //            dataGridView1.Rows.Add(); // Agrega una nueva fila si está en la última fila.
        //        }

        //        // Agrega los elementos a la siguiente fila.
        //        dataGridView1.Rows[currentRow + 1].Cells[0].Value = "Elemento 1";
        //        dataGridView1.Rows[currentRow + 1].Cells[1].Value = "Elemento 2";
        //        dataGridView1.Rows[currentRow + 1].Cells[2].Value = "Elemento 3";

        //        // Establece el foco en la primera celda de la nueva fila.
        //        dataGridView1.CurrentCell = dataGridView1.Rows[currentRow + 1].Cells[0];
        //    }
        //}

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
            DataGridViewCell precioo = dataGridView1.Rows[e.RowIndex].Cells[1];
            DataGridViewCell canti = dataGridView1.Rows[e.RowIndex].Cells[2];
            MessageBox.Show(canti.Value.ToString());
            sub -= Convert.ToInt32(precioo.Value) * Convert.ToInt32(canti.Value);
            iva = (sub * .16);
            total = (iva + sub);
            label7.Text = iva.ToString();
            label6.Text = sub.ToString();
            label8.Text = total.ToString();
            dataGridView1.Rows.Remove(fila);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == (char)Keys.Escape)
            //{
            //    Cobrar();
            //    ActualizarStock();
            //    sub = 0;
            //    cant = 0;
            //    AsignarPrecios();
            //    limpiar();
            //}
            //else if ((int)e.KeyChar == (int)Keys.Enter && textBox1.Text != null)
            //{
            //    AgregarProductos(textBox1.Text);
            //    AsignarPrecios();
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
            DataGridViewCell precioo = dataGridView1.Rows[e.RowIndex].Cells[1];
            DataGridViewCell canti = dataGridView1.Rows[e.RowIndex].Cells[2];
            MessageBox.Show(canti.Value.ToString());
            sub -= Convert.ToInt32(precioo.Value) * Convert.ToInt32(canti.Value);
            iva = (sub * .16);
            total = (iva + sub);
            label7.Text = iva.ToString();
            label6.Text = sub.ToString();
            label8.Text = total.ToString();
            dataGridView1.Rows.Remove(fila);
        }
        private void AgregarProductos(string text)
        {
            int idProducto;
            int cantidad;
            if (text.Contains("*"))
            {
                String[] div = text.Split('*');
                idProducto = Convert.ToInt32(div[0]);
                cantidad = Convert.ToInt32(div[1]);
            }
            else
            {
                idProducto = Convert.ToInt32(text);
                cantidad = 1;
            }
            conexion();
            String consulta = "SELECT Nombre, Precio FROM Productos WHERE IdPorducto=" + idProducto;
            SqlDataAdapter adap = new SqlDataAdapter(consulta, con);
            DataTable productos = new DataTable();
            adap.Fill(productos);

            if (cant == 0)
            {
                MessageBox.Show("Lo siento, el producto esta agotado");
            }



            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Precio", typeof(decimal));
                dt.Columns.Add("Cantidad", typeof(int));

                dataGridView1.DataSource = dt;
            }
            
            DataRow row = dt.NewRow();
            row["Nombre"] = productos.Rows[0]["Nombre"];
            row["Precio"] = productos.Rows[0]["Precio"];
            row["Cantidad"] = cantidad;
            dt.Rows.Add(row);

            pre = Convert.ToDouble(productos.Rows[0]["precio"]);
            cant = cantidad;

         
            dataGridView1.DataSource = dt;


            con.Close();

            textBox1.Clear();
        }

        private void compra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Cobrar();
                ActualizarStock();
                sub = 0;
                cant = 0;
                AsignarPrecios();
                limpiar();
            }
            else if ((int)e.KeyChar == (int)Keys.Enter && textBox1.Text != null)
            {
                AgregarProductos(textBox1.Text);
                AsignarPrecios();
            }
        }               

        public void conexion()
        {
            try
            {
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jenni\\source\\repos\\Diseño\\Database1.mdf;Integrated Security=True ";
                con.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("error" + ex);
            };
        }
        private void Cobrar()
        {
            try
            {
                conexion();
                String consulta = "INSERT INTO ventas3( total,fecha) VALUES(" + total + ",'"+DateTime.Today.Month +" / " + DateTime.Today.Day+" / "+ DateTime.Today.Year + "')";
                SqlCommand coman = new SqlCommand(consulta, con);
                coman.ExecuteNonQuery();                                            

                foreach (DataRow row in dt.Rows)
                {
                    string nombreProducto = row["Nombre"].ToString();
                    int cantidadVendida = Convert.ToInt32(row["Cantidad"]);

                    String consultaProductos = "Update Productos SET Cantidad = Cantidad - " + cantidadVendida + " WHERE Nombre = '" + nombreProducto + "'";
                    SqlCommand comanProductos = new SqlCommand(consultaProductos, con);

                    comanProductos.ExecuteNonQuery();
                }

                con.Close();
                MessageBox.Show("Venta realizada correctamente");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("error" + ex);
            }
        }
        private void AsignarPrecios()
        {
            sub = sub + (cant * pre);
            iva = (sub * .16);
            total = (iva + sub);
            label7.Text = iva.ToString();
            label6.Text = sub.ToString();
            label8.Text = total.ToString();
        }
        private void ActualizarStock()
        {

        }
        private void limpiar()
        {
            label7.Text = string.Empty;
            label6.Text = string.Empty;
            label8.Text = string.Empty;
            dataGridView1.DataSource = null;
            dt = null;
        }
    }
}

