using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Diseño
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer_Fecha.Enabled = true;
        }
        
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width==125)
            {
                timer_ocultar.Enabled = true;
            }
            else
            {
                timer_mostrar.Enabled = true;
            }
        }

        private void timer_mostrar_Tick(object sender, EventArgs e)
        {
            if (panelMenu.Width >= 125)
            {
                timer_mostrar.Enabled = false; //el timer es como un for 
            }
            else
            {
                panelMenu.Width = panelMenu.Width + 20;
            }
        }

        private void timer_ocultar_Tick(object sender, EventArgs e)
        {
            if (panelMenu.Width <= 60)
            {
                timer_ocultar.Enabled = false; //el timer es como un for 
            }
            else
            {
                panelMenu.Width = panelMenu.Width - 25;
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            panelTrabajo.Controls.Clear();
            //constructor para crear la ventana clientes
            clientes vclientes = new clientes();
            vclientes.TopLevel = false;
            //agrega la ventana al panel
            panelTrabajo.Controls.Add(vclientes);
            //muestra la ventana en el panel
            vclientes.Show();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            panelTrabajo.Controls.Clear();
            compra vcompra = new compra();
            vcompra.TopLevel = false;
            panelTrabajo.Controls.Add(vcompra);
            vcompra.Show();

        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            panelTrabajo.Controls.Clear();
            factura vfactura = new factura();
            vfactura.TopLevel = false;
            panelTrabajo.Controls.Add(vfactura);
            vfactura.Show();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            panelTrabajo.Controls.Clear();
            reporte vreporte = new reporte();
            vreporte.TopLevel = false;
            panelTrabajo.Controls.Add(vreporte);
            vreporte.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Fecha_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
