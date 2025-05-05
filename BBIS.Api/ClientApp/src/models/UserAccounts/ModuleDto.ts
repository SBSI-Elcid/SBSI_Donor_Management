export interface ModuleDto {
    Icon: string;
    IsParentMenu: boolean;
    Link: string;
    Menu: string;
    ModuleId: Guid;
    OrderNo: number;
    SubMenuItems: Array<ModuleDto>;
    Toggle: boolean;
    ToggleChild: boolean;
    ParentModuleId: Guid | null;
}