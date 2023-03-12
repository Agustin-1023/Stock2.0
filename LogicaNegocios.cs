using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock2._0
{
    public class LogicaNegocios
    {
        private AccesoBaseDatos _accesoBaseDatos;

        public LogicaNegocios()
        {
            _accesoBaseDatos = new AccesoBaseDatos();
        }
       public Clientes GuardarCliente(Clientes clientes)
        {
            _accesoBaseDatos.InsertCliente(clientes);
            return clientes;
        }
        public List<Clientes> GetClientes(string buscar_cliente = null)
        {
            return _accesoBaseDatos.GetClientes(buscar_cliente);
        }
        public Sucursal GuardarSucursal(Sucursal sucursal)
        {
            _accesoBaseDatos.InsertarSucursal(sucursal);
            return sucursal;
        }
        public Equipos GuardarEquipo(Equipos equipos)
        {
            _accesoBaseDatos.InsertEquipos(equipos);
            return equipos;
        }
        public List<Sucursal> GetSucursals(int buscar_sucursal = 0)
        {
            return _accesoBaseDatos.GetSucursales(buscar_sucursal);
        }
        public List<Equipos> GetEquipos(int buscar_equipo = 0)
        {
            return _accesoBaseDatos.GetEquipos();
        }
        public Modulos GuardarModulo(Modulos modulos)
        {
            _accesoBaseDatos.InsertModulos(modulos);
            return modulos;
        }
        public List<Modulos> GetModulos()
        {
            return _accesoBaseDatos.GetModulos();
        }
        public List<Modulos> GetModulos(int buscar_modulos = 0)
        {
            return _accesoBaseDatos.GetModulos();
        }

        public List<Repuestos> GetRepuestos(int buscar_repuestos = 0)
        {
            return _accesoBaseDatos.GetRepuestos();
        }
        public void AgregarModulos(int id_equipo = 0 , int id_modulo = 0)
        {
            _accesoBaseDatos.AgregarModulos(id_equipo, id_modulo);
        }
        public List<Equipos> GetModelosUnicos()
        {
            return _accesoBaseDatos.Modelos_unicos();
        }
    }
}
