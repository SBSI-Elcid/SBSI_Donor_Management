export interface IDonorMedicalHistoryDto {
    MedicalQuestionnaireId: number,
    Answer: string,
    Remarks: string,
    HeaderText: string,
    QuestionText: string
}
  
export class DonorMedicalHistoryDto implements IDonorMedicalHistoryDto {
    MedicalQuestionnaireId = 0;
    Answer = "";
    Remarks = "";
    HeaderText = "";
    QuestionText = "";
}