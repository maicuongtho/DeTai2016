using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTU.Webgen
{
    public class HocPhan
    {
        String MaHocPhan;
        String TenHocPhan;
        String KhoiLuongHP;
        String HPHocTruoc;
        String MucTieu;
        String NoiDungTomTat;
        class DanhGia{
            String Id;
            String TenHinhThuc;
            String MoTaThem;
            String TrongSo;
            String HinhThucLamBai;
            public DanhGia() { }
                
        }
        List<DanhGia> dsDanhGiaHocPhan;
        class TaiLieuThamKhao {
            String Id;
            String TenTaiLieu;
            String TacGia;
            String NamXuatBan;
            String NhaXuatBan;
            String DiaChiKhaiThac;
            String MucDichSuDung;
        }
        List<TaiLieuThamKhao> dsTaiLieuThamKhao;
        String DeCuongHP;
        class ChuDeHP {
            
        }
        List<ChuDeHP> dsChuDe;
    }
}
