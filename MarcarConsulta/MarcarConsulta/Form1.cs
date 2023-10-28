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


namespace MarcarConsulta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Configurar a string de conexão: está conexão é feita apenas quando é no windows authenticador
                string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=Vida;Integrated Security=True";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Aqui você pode executar consultas ou outras operações no banco de dados

                    // Não é mais necessário mostrar uma mensagem de sucesso, pois a conexão é aberta automaticamente ao iniciar o aplicativo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao servidor SQL: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
    }
}
