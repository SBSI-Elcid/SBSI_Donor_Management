export interface IMedicalQuestionnaireDto {
    MedicalQuestionnaireId: number,
    HeaderText: string,
    QuestionTagalogText: string,
    QuestionEnglishText: string,
    OrderNo: number,
    GenderOption: string,
}

export class MedicalQuestionnaireDto implements IMedicalQuestionnaireDto{
    MedicalQuestionnaireId: number = 0;
    HeaderText: string = "";
    QuestionTagalogText: string = "";
    QuestionEnglishText: string = "";
    OrderNo: number = 0;
    GenderOption: string = "";
}
  