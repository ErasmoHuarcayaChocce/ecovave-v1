namespace ecovave.common
{
    public class Utilitario
    {
        #region Remueve el ultimo caracter
        public string RemoverUltimoCaracter(string mensaje)
        {
            mensaje = mensaje.Substring(0, mensaje.Length - 1);
            return mensaje;
        }
        #endregion
    }
}
