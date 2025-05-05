import { SignatoryReportDto } from './../Signatories/SignatoryReportDto';
import { BloodScreeningTestOrderDto } from "../TestOrder/BloodScreeningTestOrderDto";
import { BloodTypingTestOrderDto } from "../TestOrder/BloodTypingTestOrderDto";
import { CoombsTestOrderDto } from "../TestOrder/CoombsTestOrderDto";
import { CrossMatchTestOrderDto } from "../TestOrder/CrossMatchTestOrderDto";

export interface TestOrderReportDto {
    InstitutionName: string;
    TestOrderNumber: string;
    TestCompleted: string;
    DateCreated: string;
    PatientName: string;
    BloodType: string;
    PatientIdNo: string;
    Gender: string;
    CivilStatus: string;
    PerformedBy: SignatoryReportDto;
    ReviewedBy: SignatoryReportDto;
    ValidatedBy: SignatoryReportDto;
    Phatologist: SignatoryReportDto;
    CoombsTestOrder: CoombsTestOrderDto | null;
    BloodScreeningTestOrder: BloodScreeningTestOrderDto | null;
    BloodTypingTestOrder: BloodTypingTestOrderDto | null;
    CrossMatchTestOrders: Array<CrossMatchTestOrderDto>;
}