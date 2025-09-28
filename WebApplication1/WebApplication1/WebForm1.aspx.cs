using System;

namespace WebApplication1
{
    public partial class Game : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Code chạy khi load trang lần đầu
            if (!IsPostBack)
            {
                // Có thể truyền dữ liệu xuống client nếu cần
                // Ví dụ: Response.Write("<script>alert('Game Loaded');</script>");
            }
        }
    }
}
