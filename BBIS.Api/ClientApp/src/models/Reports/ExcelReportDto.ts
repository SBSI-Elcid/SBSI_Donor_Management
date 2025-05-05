export class ExcelReportDto<T> {
    Columns: Array<ExcelColumns> = [];
    Rows: Array<T> = [];
}

export class ExcelColumns
{
    label: string = '';
    field: string = '';
}