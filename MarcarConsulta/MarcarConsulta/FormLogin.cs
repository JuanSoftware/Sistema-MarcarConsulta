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
    public partial class FormLogin : Form
    {
        string connectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=Vida;Integrated Security=True";
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string senha = txtSenha.Text;
            string checkboxSelecionada = GetSelectedCheckBox();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "";
                string tableName = "";

                // Determine a tabela com base na seleção do checkbox
                if (checkboxSelecionada == "Cliente")
                {
                    tableName = "Client";
                }
                else if (checkboxSelecionada == "ADM")
                {
                    tableName = "Administrator";
                }
                else if (checkboxSelecionada == "Medico")
                {
                    tableName = "Doctor";
                }

                query = $"SELECT * FROM {tableName} WHERE Login = @Login AND Password = @Senha";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Senha", senha);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Login bem-sucedido, determina o tipo de usuário
                            // Não é necessário mais verificar "Tipo" no banco de dados
                            // A informação está implícita com base na tabela consultada

                            // Fecha o formulário de login
                            this.Hide();

                            // Abre o formulário correspondente ao tipo de usuário
                            if (checkboxSelecionada == "Cliente")
                            {
                                FormCliente formCliente = new FormCliente();
                                formCliente.ShowDialog();
                            }
                            else if (checkboxSelecionada == "ADM")
                            {
                                FormADM formADM = new FormADM();
                                formADM.ShowDialog();
                            }
                            else if (checkboxSelecionada == "Medico")
                            {
                                FormMedico formMedico = new FormMedico();
                                formMedico.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Usuário não encontrado no banco de dados.");
                            LimparCampos();
                        }
                    }
                }
            }
        }

        private string GetSelectedCheckBox()
        {
            if (checkboxCliente.Checked)
                return "Cliente";
            else if (checkboxADM.Checked)
                return "ADM";
            else if (checkboxMedico.Checked)
                return "Medico";
            else
                return "";
        }

        private void LimparCampos()
        {
            txtLogin.Clear();
            txtSenha.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }





        
    
}
