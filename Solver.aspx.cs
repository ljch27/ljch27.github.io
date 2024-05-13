using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AN_2DO_PARCIAL
{
    public partial class Solver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si la solicitud es de tipo POST
            if (Request.HttpMethod == "POST")
            {
                // Leer los datos JSON enviados desde el formulario HTML
                string requestBody = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                dynamic requestData = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody);

                double[,] coefficients = requestData.coefficients.ToObject<double[,]>();
                double[] constants = requestData.constants.ToObject<double[]>();

                // Resolver el sistema de ecuaciones utilizando el método de Gauss-Seidel
                double[] solution = GaussSeidelSolver.Solve(coefficients, constants, new double[constants.Length]);

                // Enviar los resultados de vuelta como JSON
                Response.ContentType = "application/json";
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(solution));
                Response.End();
            }
        }
    }
}