namespace BBIS.Application.DTOs.Module
{
    public class ModuleDto
    {
        public Guid ModuleId { get; set; }
        public string Menu { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public int OrderNo { get; set; }
        public bool IsParentMenu { get; set; }
        public Guid? ParentModuleId { get; set; }

        public List<ModuleDto> SubMenuItems { get; set; }
    }
}
