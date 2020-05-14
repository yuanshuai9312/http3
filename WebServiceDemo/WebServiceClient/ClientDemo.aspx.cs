using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebServiceClient
{
    public partial class ClientDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //以下代码为手动编写，
        protected void btnOK_Click(object sender, EventArgs e)
        {
            ServiceTest.WebServiceSoapClient ss = new ServiceTest.WebServiceSoapClient();
            int i = Convert.ToInt32(txtValue1.Text);
            int j = Convert.ToInt32(txtValue2.Text);
            txtSum.Text = ss.Add(i, j).ToString();

        }
        //以上代码为手动编写，其他为自动生成
    }
}