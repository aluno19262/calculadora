using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session["LimpaEcra"] = true;
            Session["Operador"] = "";

            return View();
        }

        // Post: Home
        [HttpPost]
        public ActionResult Index(string visor,string bt)
        {
            
            switch (bt)
            {
                //selecionar 1 algarismo
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (visor == "0" || (bool)Session["LimpaEcra"])
                    {
                        visor = bt;
                        //marca que o visor nao deve ser limpo
                        Session["LimpaEcra"] = false;
                    }
                    else
                    {
                        visor += bt;
                    }

                    break;

                //selecionei o "+/-"

                //selecionei o ","

                //selecionei um simbolo de operaçao " +,-,x,:"
                case "+":
                case "-":
                case "x":
                case ":":
                case "=":

                    //ja pressionei ,alguma vez, um sinal de operador ?
                    if ((string)Session["Operador"] == "")
                    {
                        //é a primeira vez que pressiono 1 operador
                        Session["Operador"] = bt;
                        //guardar o valor do 1º operando
                        Session["PrimeiroOperando"] = visor;
                        //marcar a calculadora (visor) para ser reiniciada 
                        Session["LimpaEcra"] = true;

                    }
                    else
                    {
                        //ja ha 2 operandos e 1 operador 
                        //ja se pode executar a operaçao aritmetica

                        //recuperar os dados da operaçao aritmetica 
                        double operando1 = Convert.ToDouble(Session["PrimeiroOperando"]);
                        double operando2 = Convert.ToDouble(visor);
                        string operador = (string)Session["Operador"];
                        //agora ja tenho os dados para fazer a operaçao
                        switch (operador)
                        {
                            case "+":
                                visor = operando1 + operando2+"";
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                break;
                            case "x":
                                visor = operando1 * operando2 + "";
                                break;
                            case ":":
                                visor = operando1 / operando2 + "";
                                break;
                        }//fim da operaçao
                         //preparar a calculadora para continuar as operaçoes
                         //guardar operador
                        Session["Operador"] = bt;
                        //guardar o valor do 1º operando
                        Session["PrimeiroOperando"] = visor;
                        //marcar a calculadora (visor) para ser reiniciada 
                        Session["LimpaEcra"] = true;
                    }
                    break;
                case "C":
                    Session["Operador"] = "";
                    //guardar o valor do 1º operando
                    Session["PrimeiroOperando"] = "";
                    visor = "";
                    break;

                case "+/-":

                    double oper = Convert.ToDouble(visor);
                    double oper1;
                    oper1 = -1 * oper;
                    visor=oper1.ToString();
                    
                    break;

                case ",":
                    if (!visor.Contains(",")) visor = visor + bt;
                    break;



            }
            ViewBag.Resposta = visor;
            return View();
        }
    }
}