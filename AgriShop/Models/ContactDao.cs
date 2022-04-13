using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;

namespace AnBinhMarket.Models
{
    public class ContactDao
    {
        private readonly ApplicationDbContext _context;

        public ContactDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public LienHe GetActiveContact()
        {
            return _context.LienHes.Single(x => x.TrangThai == true);
        }
        public Guid InsertFeedBack(PhanHoi ph)
        {
            var _ph = new PhanHoi()
            {
                DiaChi = ph.DiaChi,
                Email = ph.Email,
                MaPhanHoi = ph.MaPhanHoi,
                NgayTao = DateTime.Now,
                NoiDung = ph.NoiDung,
                SoDT = ph.SoDT,
                Ten = ph.Ten
            };
            try
            {
                _context.PhanHois.Add(_ph);
                _context.SaveChanges();
                return _ph.Id;

            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
         
        }
    }
}
