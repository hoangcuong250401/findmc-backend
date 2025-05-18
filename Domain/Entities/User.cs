using Domain.Attributes;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// một người dùng trong hệ thống:
    /// nếu IsMC = true thì vừa là MC vừa là khách tìm MC
    /// nếu IsMC = false thì là khách tìm MC
    /// </summary>
    [Table("user")]
    public class User : BaseEntity
    {
        /// <summary>
        /// họ tên - lấy từ tk google 
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// email - lấy từ tk google 
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// số điện thoại - lấy từ tk google 
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsMc { get; set; }
        public int? Age { get; set; }
        /// <summary>
        /// nghệ danh
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// điểm uy tín
        /// </summary>
        public decimal Credit { get; set; }
        public Gender Gender { get; set; }
        /// <summary>
        /// có phải MC mới không
        /// </summary>
        public bool IsNewbie { get; set; }
        /// <summary>
        /// khu vực hoạt động
        /// </summary>
        public string WorkingArea { get; set; } = string.Empty;
        /// <summary>
        /// đã xác thực danh tính chưa
        /// </summary>
        public bool IsVerified { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string AvatarUrl { get; set; } = string.Empty;
        public string Facebook { get; set; } = string.Empty;
        public string Zalo { get; set; } = string.Empty; 
    }
}
    