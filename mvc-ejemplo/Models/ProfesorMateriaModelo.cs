namespace SistemaWebEscolar.Models
{
    public class ProfesorMaterialModelo
    {
        public int IdProfesor { get; set; }          // Id del profesor
        public string NombreProfesor { get; set; }  // Nombre del profesor

        public int IdGrado { get; set; }             // Id del grado seleccionado
        public string NombreGrado { get; set; }      // Nombre del grado

        public int IdMateria { get; set; }           // Id de la materia seleccionada
        public string NombreMateria { get; set; }    // Nombre de la materia
    }
}