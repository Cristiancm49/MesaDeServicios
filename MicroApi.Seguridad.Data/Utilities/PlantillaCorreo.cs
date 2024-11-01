using System;

namespace MicroApi.Seguridad.Data.Utilities
{
    public class PlantillaCorreo
    {
        private readonly string plantilla;

        public PlantillaCorreo()
        {
            plantilla = @"
                <html>
                    <head>
                        <title></title>
                        <style type='text/css'>
                            .style1 { width: 90px; }
                            .style2 { width: 734px; }
                            .style3 { width: 90px; height: 67px; }
                            .style4 { width: 734px; height: 67px; }
                            .style5 { height: 67px; }
                            .style6 { width: 199px; }
                            .style7 { width: 90px; height: 427px; }
                            .style8 { width: 734px; height: 427px; }
                            .style9 { height: 427px; }
                            .auto-style1 { height: 67px; width: 122px; }
                            .auto-style2 { height: 389px; width: 122px; }
                            .auto-style3 { width: 122px; }
                            .auto-style4 { width: 547px; height: 67px; }
                            .auto-style5 { width: 547px; height: 389px; }
                            .auto-style6 { width: 547px; }
                            .auto-style7 { width: 260px; }
                            .auto-style8 { width: 90px; height: 389px; }
                        </style>
                    </head>
                    <body>
                        <form>
                            <div>
                                <table style='width:60%;'>
                                    <tr>
                                        <td class='style3'></td>
                                        <td class='auto-style4' align='center' bgcolor='White'>
                                            <img src='https://chaira.uniamazonia.edu.co/Chaira/Resources/Images/encabezado.png' style='height: 90px; width: 600px' />
                                        </td>
                                        <td class='auto-style1' bgcolor='White'></td>
                                    </tr>
                                    <tr>
                                        <td class='auto-style8' bgcolor='White'></td>
                                        <td class='auto-style5'>
                                            <div style='margin: auto; border-style: none; border-width: thin; padding: inherit; height: auto; font-family: Arial, Helvetica, sans-serif; font-weight: normal; font-style: normal; background-color: #FFFFFF; width: 539px; font-size: small;'>
                                                <br /> {0}
                                            </div>
                                        </td>
                                        <td class='auto-style2'></td>
                                    </tr>
                                    <tr>
                                        <td class='style3'></td>
                                        <td class='auto-style4' align='center' bgcolor='White'>
                                            <img src='https://chaira.uniamazonia.edu.co/Chaira/Resources/Images/encabeza.png' style='height: 120px; width: 800px' />
                                        </td>
                                        <td class='auto-style1' bgcolor='White'></td>
                                    </tr>
                                </table>
                            </div>
                        </form>
                    </body>
                </html>";
        }

        public string ObtenerPlantillaCorreo(string contenido)
        {
            return plantilla.Replace("{0}", contenido);
        }
    }
}