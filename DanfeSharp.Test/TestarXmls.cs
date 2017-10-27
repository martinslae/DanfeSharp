using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using DanfeSharp;
using DanfeSharp.Model;
using System.Diagnostics;

namespace DanfeSharp.Test
{
    [TestClass]
    public class TestarXmls
    {
        [TestMethod]
        public void TestarArquivoXml310()
        {
            DanfeViewModel model = DanfeViewModel.CreateFromXmlString(Properties.Resources.NFe52171004621624000349550000001227291432126856_nfe);
            Assert.AreEqual(model.Destinatario.Nome, "CLIENTE - Cod. 0374941");            
        }

        [TestMethod]
        public void TestarValorPIS()
        {
            DanfeViewModel model = DanfeViewModel.CreateFromXmlString(Properties.Resources.NFe52171004621624000349550000001227291432126856_nfe);
            Assert.AreEqual(model.ValorPis, 1d);
        }

        [TestMethod]
        public void TestarValorCOFINS()
        {
            DanfeViewModel model = DanfeViewModel.CreateFromXmlString(Properties.Resources.NFe52171004621624000349550000001227291432126856_nfe);
            Assert.AreEqual(model.ValorCofins, 4.63d);
        }

        [TestMethod]
        public void XmlPasta()
        {           
            var arquivos = Directory.EnumerateFiles("../../XmlTestes", "*.xml");

            foreach (var arquivo in arquivos)
            {
                try
                {
                    DanfeViewModel model = DanfeViewModel.CreateFromXmlFile(arquivo);
                    using (DanfeDocumento danfe = new DanfeDocumento(model))
                    {
                        danfe.Gerar();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            danfe.Salvar(ms);
                        }                        
                    }
                }
                catch(Exception e)
                {
                    Debugger.Break();
                }
                
            }
        }
    }
}
