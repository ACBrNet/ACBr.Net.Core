// ***********************************************************************
// Assembly         : ACBr.Net.Core
// Author           : RFTD
// Created          : 03-23-2014
//
// Last Modified By : RFTD
// Last Modified On : 04-25-2015
// ***********************************************************************
// <copyright file="CalcDigito.cs" company="ACBr.Net">
// Esta biblioteca � software livre; voc� pode redistribu�-la e/ou modific�-la
// sob os termos da Licen�a P�blica Geral Menor do GNU conforme publicada pela
// Free Software Foundation; tanto a vers�o 2.1 da Licen�a, ou (a seu crit�rio)
// qualquer vers�o posterior.
//
// Esta biblioteca � distribu�da na expectativa de que seja �til, por�m, SEM
// NENHUMA GARANTIA; nem mesmo a garantia impl�cita de COMERCIABILIDADE OU
// ADEQUA��O A UMA FINALIDADE ESPEC�FICA. Consulte a Licen�a P�blica Geral Menor
// do GNU para mais detalhes. (Arquivo LICEN�A.TXT ou LICENSE.TXT)
//
// Voc� deve ter recebido uma c�pia da Licen�a P�blica Geral Menor do GNU junto
// com esta biblioteca; se n�o, escreva para a Free Software Foundation, Inc.,
// no endere�o 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.
// Voc� tamb�m pode obter uma copia da licen�a em:
// http://www.opensource.org/licenses/lgpl-license.php
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using ACBr.Net.Core.Enum;
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.Core
{
    /// <summary>
    /// Class CalcDigito. This class cannot be inherited.
    /// </summary>
    public class CalcDigito
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CalcDigito" /> class.
        /// </summary>
        public CalcDigito()
        {
            Documento = string.Empty;
            DigitoFinal = 0;
            SomaDigitos = 0;
            MultiplicadorInicial = 2;
            MultiplicadorFinal = 9;
            FormulaDigito = CalcDigFormula.Modulo11;
        }

        #endregion Constructor
        
        #region Propriedades

        /// <summary>
        /// Gets or sets the documento.
        /// </summary>
        /// <value>The documento.</value>
        public string Documento { get; set; }
        /// <summary>
        /// Gets or sets the multiplicador inicial.
        /// </summary>
        /// <value>The multiplicador inicial.</value>
        public int MultiplicadorInicial { get; set; }
        /// <summary>
        /// Gets or sets the multiplicador final.
        /// </summary>
        /// <value>The multiplicador final.</value>
        public int MultiplicadorFinal { get; set; }
        /// <summary>
        /// Gets or sets the multiplicador atual.
        /// </summary>
        /// <value>The multiplicador atual.</value>
        public int MultiplicadorAtual { get; set; }
        /// <summary>
        /// Gets or sets the digito final.
        /// </summary>
        /// <value>The digito final.</value>
        public int DigitoFinal { get; set; }
        /// <summary>
        /// Gets or sets the modulo final.
        /// </summary>
        /// <value>The modulo final.</value>
        public int ModuloFinal { get; set; }
        /// <summary>
        /// Gets or sets the soma digitos.
        /// </summary>
        /// <value>The soma digitos.</value>
        public int SomaDigitos { get; set; }
        /// <summary>
        /// Gets or sets the formula digito.
        /// </summary>
        /// <value>The formula digito.</value>
        public CalcDigFormula FormulaDigito { get; set; }


        #endregion Propriedades

        #region Methods

        /// <summary>
        /// Calcula o digito verificador
        /// </summary>
        public void Calcular()
        {
            Documento = Documento.Trim();
            SomaDigitos = 0;
            DigitoFinal = 0;
            ModuloFinal = 0;
            var vlrBase = 0;

            if (MultiplicadorAtual >= MultiplicadorInicial && MultiplicadorAtual <= MultiplicadorFinal)
                vlrBase = MultiplicadorAtual;
            else
                vlrBase = MultiplicadorInicial;

            var tamanho = Documento.Length - 1; ;

            //Calculando a Soma dos digitos de traz para diante, multiplicadas por BASE

            for (var i = 0; i < tamanho; i++)
            {
                var N = Documento[tamanho - i].ToInt32();
                var vlrCalc = (N * vlrBase);

                if (FormulaDigito == CalcDigFormula.Modulo10 && vlrCalc > 9)
                {
                    var vlrCalcStr = vlrCalc.ToString();
                    vlrCalc = vlrCalcStr[0].ToInt32() + vlrCalcStr[1].ToInt32();
                }

                SomaDigitos += vlrCalc;

                if (MultiplicadorInicial > MultiplicadorFinal)
                {
                    vlrBase--;
                    if (vlrBase < MultiplicadorFinal)
                        vlrBase = MultiplicadorInicial;
                }
                else
                {
                    vlrBase++;
                    if (vlrBase > MultiplicadorFinal)
                        vlrBase = MultiplicadorInicial;
                }
            }

            switch (FormulaDigito)
            {
                case CalcDigFormula.Modulo11:
                    ModuloFinal = SomaDigitos % 11;

                    if (ModuloFinal < 2)
                        DigitoFinal = 0;
                    else
                        DigitoFinal = 11 - ModuloFinal;
                    break;

                case CalcDigFormula.Modulo10PIS:
                    ModuloFinal = (SomaDigitos % 11);
                    DigitoFinal = 11 - ModuloFinal;
                    if (DigitoFinal >= 10)
                        DigitoFinal = 0;
                    break;

                case CalcDigFormula.Modulo10:
                    ModuloFinal = (SomaDigitos % 10);
                    DigitoFinal = 10 - ModuloFinal;
                    if (DigitoFinal >= 10)
                        DigitoFinal = 0;
                    break;

	            default:
		            throw new ArgumentOutOfRangeException(@"FormulaDigito");
            }
        }

	    /// <summary>
	    /// Calculoes the padrao.
	    /// </summary>
	    public void CalculoPadrao()
	    {
		    MultiplicadorInicial = 2;
		    MultiplicadorFinal = 9;
		    MultiplicadorAtual = 0;
		    FormulaDigito = CalcDigFormula.Modulo11;
	    }
		
		#endregion Methods		
	}
}
