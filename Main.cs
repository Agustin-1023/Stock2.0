using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock2._0
{
    public partial class Main : Form
    {
        private LogicaNegocios _logicaNegocios;
        //int id_cliente = -1;
        //int id_sucursal = -1;
        public Main()
        {
            InitializeComponent();
            _logicaNegocios = new LogicaNegocios();
        }
        private void modulo_A_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void modelo_equipo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void ingresar_suc_Click(object sender, EventArgs e)
        {
            Guardar_Cliente();

        }
        public void Guardar_Sucursal(int id_cliente)
        {

            Sucursal sucursal = new Sucursal();

            sucursal.nombre_cliente = id_cliente;
            sucursal.nombre_sucursal = nombre_suc.Text.ToUpper();
            sucursal.direccion_sucursal = direccion_suc.Text.ToUpper();

            _logicaNegocios.GuardarSucursal(sucursal);
        }
        public void Guardar_Sucursal()
        {

            Sucursal sucursal = new Sucursal();


            sucursal.nombre_sucursal = nombre_suc.Text.ToUpper();
            sucursal.direccion_sucursal = direccion_suc.Text.ToUpper();

            _logicaNegocios.GuardarSucursal(sucursal);
        }
        private void Guardar_Cliente(string buscar_cliente = null)
        {
            Clientes cliente = new Clientes();
            cliente.nombre_cliente = nombre_cliente.Text.ToUpper();
            List<Clientes> clientes = _logicaNegocios.GetClientes();
            bool existe = clientes.Any(p => p.nombre_cliente == nombre_cliente.Text.ToUpper() || p.nombre_cliente == nombre_cliente.Text);
            if(existe == true)
            {
                MessageBox.Show("existe el cliente ya existe");
            }
            else
            {
                _logicaNegocios.GuardarCliente(cliente);
            }
            nombre_cliente.Text = "";
            nombre_suc.Text = "";
            direccion_suc.Text = "";
            Lista_Clientes();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Lista_Clientes();
            Lista_Equipos();
        }

        private void Lista_Clientes(string buscar_cliente = null)
        {
            List<Clientes> clientes = _logicaNegocios.GetClientes(buscar_cliente);
            dataGridView1.DataSource = clientes;
            combo_ing_suc.Items.Clear();
            combo_cliente.Items.Clear();
            foreach (Clientes nombre_clientes in clientes)
            {
                combo_cliente.Items.Add(nombre_clientes.nombre_cliente);
                combo_ing_suc.Items.Add(nombre_clientes.nombre_cliente);
                combo_cliente_repuestos.Items.Add(nombre_clientes.nombre_cliente);
                //cliente_repuesto.Items.Add(nombre_clientes.nombre_cliente);
            }
            if (clientes.Count() != 0)
            {
                flag.Text = "BD cargada ";
                flag.BackColor = Color.Green;
            } 
        }
        private void Lista_Equipos()
        {
            lista_equipos.Items.Clear();
            List<Equipos> equipos = _logicaNegocios.GetModelosUnicos();
            foreach (Equipos lista in equipos)
            {
                lista_equipos.Items.Add(lista.modelo);
                equipo_repuesto.Items.Add(lista.modelo);
            };
        }
        private void buscar_suc_Click(object sender, EventArgs e)
        {
            List<Sucursal> sucursales = _logicaNegocios.GetSucursals();
            List<Clientes> clientes = _logicaNegocios.GetClientes();
            Sucursal sucursal = new Sucursal();
            int id_ingreso_cliente = 0;            

            foreach(Clientes id_cliente in clientes)
            {
                if(id_cliente.nombre_cliente == combo_ing_suc.Text)
                {
                    id_ingreso_cliente = id_cliente.Id;
                    break;
                }
                else
                { 
                }
            }
            sucursal.nombre_sucursal = nombre_suc.Text.ToUpper();
            sucursal.direccion_sucursal = direccion_suc.Text.ToUpper();
            sucursal.nombre_cliente = id_ingreso_cliente;
            foreach(Sucursal nombre_sucursal in sucursales)
            {
                if (nombre_suc.Text == nombre_sucursal.nombre_sucursal)
                {
                    MessageBox.Show("La sucursal ya existe");
                }
                else
                {
                    _logicaNegocios.GuardarSucursal(sucursal);
                    break;
                }
            }
            nombre_cliente.Text = "";
            nombre_suc.Text = "";
            direccion_suc.Text = "";
            Lista_Clientes();
        }

        private void combo_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_sucursal.Items.Clear();
            string nada = null;
            int indice = combo_cliente.SelectedIndex;
            int id_cliente = -1;
            string nombre_select_cliente = combo_cliente.Items[indice].ToString();
            List<Clientes> clientes = _logicaNegocios.GetClientes(nada);

            foreach (Clientes nom_clientes in clientes)
            {
                if (nom_clientes.nombre_cliente == nombre_select_cliente)
                {
                    id_cliente = nom_clientes.Id;
                }
            }
            List<Sucursal> lista_sucursales = _logicaNegocios.GetSucursals();
            foreach(Sucursal encontrar_sucursales in lista_sucursales)
            {
                if(encontrar_sucursales.nombre_cliente == id_cliente)
                {
                    combo_sucursal.Items.Add(encontrar_sucursales.nombre_sucursal);
                }
            }

        }
        private void Lista_Sucursales(int buscar_sucursal = 0)
        {
            
            List<Sucursal> sucursales = _logicaNegocios.GetSucursals(buscar_sucursal);
            foreach (Sucursal nombre_sucursales in sucursales)
            {
                if (nombre_sucursales.nombre_cliente == buscar_sucursal)
                {
                    flag3.Text = Convert.ToString(buscar_sucursal);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //LLama a la funcion para agregar un Equipo
            Lista_Equipo();
        }
        private void Lista_Equipo(string buscar_equipo = null)
        {
            Equipos equipos = new Equipos();
            int indice = combo_sucursal.SelectedIndex;
            string nombre_select_cliente = combo_sucursal.Items[indice].ToString();
            int id_sucursal = 0;
            MessageBox.Show(nombre_select_cliente);
            List<Sucursal> sucursales = _logicaNegocios.GetSucursals();
            foreach (Sucursal nombre_sucursales in sucursales)
            { 
                if (nombre_sucursales.nombre_sucursal == nombre_select_cliente)
                {
                    id_sucursal = nombre_sucursales.id_sucursal;
                }
                else
                {
                }
            }
            equipos.id_sucursal = id_sucursal;
            equipos.modelo = modelo_equipo.Text;
            equipos.numero_serie = numero_serie.Text;
            equipos.ip = ip.Text;
            equipos.mac = mac.Text;
            int id_modelo_exist = -1;
            int id_equipo_nuevo = -1;
            List<Equipos> lista_equipos = _logicaNegocios.GetEquipos();
            foreach (Equipos encontrar_modelo in lista_equipos)
            {
                if (encontrar_modelo.modelo == modelo_equipo.Text)
                {
                    id_modelo_exist = encontrar_modelo.id;
                    break;
                }
                else
                {
                }
            }
            _logicaNegocios.GuardarEquipo(equipos);
            List<Equipos> lista_equipos_nueva = _logicaNegocios.GetEquipos();
            foreach (Equipos encontrar_modelo in lista_equipos_nueva)
            {
                if (encontrar_modelo.numero_serie == numero_serie.Text)
                {
                    id_equipo_nuevo = encontrar_modelo.id;
                    break;
                }
                else
                {
                }
            }
            _logicaNegocios.AgregarModulos(id_modelo_exist, id_equipo_nuevo);
            Limpiar_campos_equipos();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // pesta;a ingreso sucursales combo de sucursales y actualizacion del datagrid
            int indice = combo_ing_suc.SelectedIndex;

            string nombre_select_cliente = combo_ing_suc.Items[indice].ToString();

            List<Clientes> clientes = _logicaNegocios.GetClientes();
            List<Sucursal> sucursales = _logicaNegocios.GetSucursals();
            int id_cliente_encontrado = 0;
            foreach(Clientes filtrar_sucursales in clientes)
            {
                if(filtrar_sucursales.nombre_cliente == nombre_select_cliente)
                {
                    id_cliente_encontrado = filtrar_sucursales.Id;
                }
            }
            DGVsucursales.DataSource = sucursales.FindAll(x => x.nombre_cliente == id_cliente_encontrado);
        }

        private void Ingreso_Modulos_Click_1(object sender, EventArgs e)
        {
            Guardar_Modulos();
        }
        private void Guardar_Modulos()
        {
            List<Equipos> equipos = _logicaNegocios.GetEquipos();
            int indice = lista_equipos.SelectedIndex;
            string idequipo = lista_equipos.Items[indice].ToString();

            Modulos modulo = new Modulos();

            foreach(Equipos obtener_id_equipo in equipos)
            {
                if(obtener_id_equipo.modelo == idequipo)
                {
                    modulo.id_equipo = obtener_id_equipo.id;
                }
            }
            if(String.IsNullOrEmpty(modulo_A.Text))
            {
                MessageBox.Show("No se puede ingresar modulos sin nombre.");
            }
            else
            {
                modulo.nombre = modulo_A.Text.ToUpper();
                _logicaNegocios.GuardarModulo(modulo);
            };

            if (String.IsNullOrEmpty(modulo_B.Text))
            {
            }
            else
            {
                modulo.nombre = modulo_B.Text.ToUpper();
                _logicaNegocios.GuardarModulo(modulo);
            };

            if (String.IsNullOrEmpty(modulo_C.Text))
            {
            }
            else
            {
                modulo.nombre = modulo_C.Text.ToUpper();
                _logicaNegocios.GuardarModulo(modulo);
            };

            if (String.IsNullOrEmpty(modulo_D.Text))
            {
            }
            else
            {
                modulo.nombre = modulo_D.Text.ToUpper();
                _logicaNegocios.GuardarModulo(modulo);
            };
            if (String.IsNullOrEmpty(modulo_E.Text))
            {
            }
            else
            {
                modulo.nombre = modulo_E.Text.ToUpper();
                _logicaNegocios.GuardarModulo(modulo);
            };
            if (String.IsNullOrEmpty(modulo_F.Text))
            {
            }
            else
            {
                modulo.nombre = modulo_F.Text.ToUpper();
                _logicaNegocios.GuardarModulo(modulo);
            };
            Limpiar_campos_modulos();
        }

        private void lista_equipos_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<Modulos> modulos = _logicaNegocios.GetModulos();
            List<Equipos> equipos = _logicaNegocios.GetEquipos();
            int indice = lista_equipos.SelectedIndex;
            string modelo_equipo = lista_equipos.Items[indice].ToString();
            int id_modelo_equipo = 0;
            string modelo_equipo_elegido = null;
            foreach (Equipos id_equipo_elegido in equipos)
            {
                if (id_equipo_elegido.modelo == modelo_equipo)
                {
                    id_modelo_equipo = id_equipo_elegido.id;
                    modelo_equipo_elegido = id_equipo_elegido.modelo;
                }
                else
                {
                }
            }
            DGVmodulos.DataSource = modulos.FindAll(x => x.id_equipo == id_modelo_equipo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Limpiar_campos_modulos();
        }
        private void Limpiar_campos_modulos()
        {
            modulo_A.Clear();
            modulo_B.Clear();
            modulo_C.Clear();
            modulo_D.Clear();
            modulo_E.Clear();
            modulo_F.Clear();
        }

        private void Limpiar_tab_equipos_Click(object sender, EventArgs e)
        {
            Limpiar_campos_equipos();
        }
        private void Limpiar_campos_equipos()
        {
            modelo_equipo.Clear();
            numero_serie.Clear();
            ip.Clear();
            mac.Clear();
        }

        private void Ingresar_repuesto_Click(object sender, EventArgs e)
        {
            Guardar_Repuesto();
        }
        private void Guardar_Repuesto()
        {
            List<Repuestos> repuestos = new List<Repuestos>();

        }
        private void Lista_Equipos_Repuesto()
        {
            //equipo_repuesto.Items.Clear();
        }

        private void sucursal_repuesto_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void equipo_repuesto_MouseClick(object sender, MouseEventArgs e)
        {
            Lista_Equipos_Repuesto();
        }

        private void combo_cliente_repuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Clientes> lista_clientes = _logicaNegocios.GetClientes();
            combo_sucursal_repuestos.Items.Clear();
            int indice = combo_cliente_repuestos.SelectedIndex;
            int id_cliente = -1;
            string nombre_select_cliente = combo_cliente_repuestos.Items[indice].ToString();

            foreach (Clientes nom_clientes in lista_clientes)
            {
                if (nom_clientes.nombre_cliente == nombre_select_cliente)
                {
                    id_cliente = nom_clientes.Id;
                }
            }
            List<Sucursal> lista_sucursales = _logicaNegocios.GetSucursals();
            foreach (Sucursal encontrar_sucursales in lista_sucursales)
            {
                if (encontrar_sucursales.nombre_cliente == id_cliente)
                {
                    combo_sucursal_repuestos.Items.Add(encontrar_sucursales.nombre_sucursal);
                }
            }
        }

        private void combo_sucursal_repuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Sucursal> lista_sucursales = _logicaNegocios.GetSucursals();
            equipo_repuesto.Items.Clear();
            int indice = combo_sucursal_repuestos.SelectedIndex;
            int id_sucursal = -1;
            string nombre_select_sucursal = combo_sucursal_repuestos.Items[indice].ToString();
            foreach (Sucursal nom_sucursales in lista_sucursales)
            {
                if(nom_sucursales.nombre_sucursal == nombre_select_sucursal)
                {
                    id_sucursal = nom_sucursales.id_sucursal;
                }
            }
            List<Equipos> lista_equipos = _logicaNegocios.GetEquipos();
            foreach (Equipos encontrar_equipos in lista_equipos)
            {
                if(encontrar_equipos.id_sucursal == id_sucursal)
                {
                    equipo_repuesto.Items.Add(encontrar_equipos.modelo);
                }
            }
        }
    }
}
