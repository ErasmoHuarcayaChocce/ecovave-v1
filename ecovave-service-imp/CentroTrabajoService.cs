using ecovave.common;
using ecovave.dao.imp;
using ecovave.dao.intf;
using ecovave.model;
using ecovave.service.intf;
using minedu.tecnologia.util.lib;
using minedu.tecnologia.util.lib.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecovave.service.imp
{
    public class CentroTrabajoService : ServiceBase, ICentroTrabajoService
    {

        public CentroTrabajoService(string txtConnectionString)
        {
            base.txtConnectionString = txtConnectionString;
        }

        public async Task<IEnumerable<CentroTrabajoGrilla>> ListaCentroTrabajo(CentroTrabajoFiltro request)
        {
            try
            {
                bool requerido = ValidarRequerido(request);
                if (requerido)
                    throw new ValidationCustomException(Constante.EX01_M01_CENTRO_TRABAJO);

        
            /*
                if (catalogoItem == null)
                    throw new ValidationCustomException(Constante.EX_NIVEL_INSTANCIA_VALIDAR);
                */
         
         
                ICentroTrabajoDAO centroTrabajoDAO = new CentroTrabajoDAO(txtConnectionString);
                IEnumerable<CentroTrabajoGrilla> response = null;

                if (request.idTipoCentroTrabajo == null)
                {
                    response = await centroTrabajoDAO.ListaCentroTrabajoDREUGEL(request);
                }
                else
                {
                    response = await centroTrabajoDAO.ListaCentroTrabajo(request);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CentroTrabajoRegistro> GetCentroTrabajo(CentroTrabajoConsulta request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.codigoCentroTrabajo))
                    throw new ValidationCustomException(Constante.EX_UNPROCESSABLE_ENTITY);

                ICentroTrabajoDAO centroTrabajoDAO = new CentroTrabajoDAO(txtConnectionString);
                CentroTrabajoRegistro response = await centroTrabajoDAO.GetCentroTrabajo(request);
                if (response == null)
                    throw new NotFoundCustomException(Constante.EX_CENTRO_TRABAJO_NOT_FOUND);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidarRequerido(CentroTrabajoFiltro request)
        {
            bool requerido = false;
            if (request.idNivelInstancia <= 0) requerido = true;

            switch (request.idNivelInstancia)
            {
                case (int)TablaNivelInstancia.MINEDU:
                    if (request.idInstancia == null || request.idInstancia <= 0) requerido = true;
                    if (request.idTipoCentroTrabajo <= 0) requerido = true;
                    break;
                case (int)TablaNivelInstancia.DRE:
                    switch (request.idTipoCentroTrabajo)
                    {
                        case (int)TablaTipoCentroTrabajo.SedeAdministrativaDRE:
                            if (request.idInstancia == null && request.idInstancia <= 0) requerido = false;
                            break;
                        case (int)TablaTipoCentroTrabajo.InstitucionEducativaDRE:
                            if (request.idInstancia == null && request.idInstancia <= 0) requerido = false;
                            if (string.IsNullOrEmpty(request.institucionEducativa)) requerido = false;
                            break;
                        case (int)TablaTipoCentroTrabajo.InstitutoSuperiorDRE:
                            if (request.idInstancia == null && request.idInstancia <= 0) requerido = false;
                            if (string.IsNullOrEmpty(request.institucionEducativa)) requerido = false;
                            break;
                    }
                    break;
                case (int)TablaNivelInstancia.UGEL:
                    switch (request.idTipoCentroTrabajo)
                    {
                        case (int)TablaTipoCentroTrabajo.SedeAdministrativaUGEL:
                            if (request.idInstancia == null && request.idInstancia > 0) requerido = false;
                            if (request.idSubinstancia == null && request.idSubinstancia > 0) requerido = false;
                            break;
                        case (int)TablaTipoCentroTrabajo.InstitucionEducativaUgel:
                            if (request.idInstancia == null && request.idInstancia <= 0) requerido = false;
                            if (request.idSubinstancia == null && request.idSubinstancia <= 0) requerido = false;
                            if (string.IsNullOrEmpty(request.institucionEducativa)) requerido = false;
                            break;
                    }
                    break;
            }
            return requerido;
        }

        public async Task<int> CrearCentroTrabajoReplica(CentroTrabajoReplica request)
        {
            int response = 0;
            try
            {


                CentroTrabajo centroTrabajo = new CentroTrabajo
                { 
                    idOtraInstancia = request.idOtraInstancia,

                    codigoCentroTrabajo = request.codigoCentroTrabajo,
                    activo = request.activo,
                    fechaCreacion=request.fechaCreacion,
                    usuarioCreacion=request.usuarioCreacion,
                    ipCreacion=request.ipCreacion,
                    fechaModificacion=request.fechaModificacion,
                    usuarioModificacion=request.usuarioModificacion,
                    ipModificacion=request.ipModificacion
                };

                ICentroTrabajoDAO centroTrabajoDAO = new CentroTrabajoDAO(txtConnectionString);
                if (request.codigoCentroTrabajo == null)
                    throw new ValidationCustomException(Constante.EX_CENTRO_TRABAJO_NOT_FOUND);
                var CentroCentroTrabajo = await centroTrabajoDAO.GetCentroTrabajoByCodigo(request.codigoCentroTrabajo, null);
                if (CentroCentroTrabajo == null)
                {
                    response = await centroTrabajoDAO.CrearCentroTrabajoReplica(centroTrabajo);
                    if (response == 0)
                    {
                        throw new CustomException(Constante.EX_CENTRO_TRABAJO_CREATE);
                    }
                }
                else
                {
                    centroTrabajo.idCentroTrabajo = CentroCentroTrabajo.idCentroTrabajo;
                    response = await centroTrabajoDAO.ModificarCentroTrabajoReplica(centroTrabajo);
                    if (response == 0)
                    {
                        throw new CustomException(Constante.EX_CENTRO_TRABAJO_UPDATE);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public async Task<int> ModificarCentroTrabajoReplica(CentroTrabajoReplica request)
        {
            try
            {
                return await CrearCentroTrabajoReplica(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DesactivarCentroTrabajoReplica(CentroTrabajoReplica request)
        {
            try
            {
                CentroTrabajo centroTrabajo = new CentroTrabajo
                {
                    codigoCentroTrabajo = request.codigoCentroTrabajo,
                    activo = request.activo,
                    fechaModificacion = DateTime.Now,
                    usuarioModificacion = request.usuarioModificacion,
                    ipModificacion = request.ipModificacion
                };
                ICentroTrabajoDAO centroTrabajoDAO = new CentroTrabajoDAO(txtConnectionString);
                var CentroCentroTrabajo = await centroTrabajoDAO.GetCentroTrabajoByCodigo(request.codigoCentroTrabajo, null);
                if (CentroCentroTrabajo == null) return 1;

                centroTrabajo.idCentroTrabajo = CentroCentroTrabajo.idCentroTrabajo;
                int response = await centroTrabajoDAO.DesactivarCentroTrabajoReplica(centroTrabajo);
                if (response == 0)
                {
                    throw new CustomException(Constante.EX_CENTRO_TRABAJO_DELETE);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CentroTrabajoResponse> GetCentroTrabajoByCodigo(string codigoCentroTrabajo, bool? activo)
        {
            try
            {
                if (string.IsNullOrEmpty(codigoCentroTrabajo))
                    throw new ValidationCustomException(Constante.EX_PARAMETROS_INCORRECTOS);

                ICentroTrabajoDAO centroTrabajoDAO = new CentroTrabajoDAO(txtConnectionString);
                var response = await centroTrabajoDAO.GetCentroTrabajoByCodigo(codigoCentroTrabajo, activo);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
