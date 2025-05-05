const GenderOptions: any = [
    { text: 'Male', value: 'Male'},
    { text: 'Female', value: 'Female'},
];

const RoleOptions: any = [
    { text: 'Admin', value: '489abc53-af8a-4914-bd21-7d99c26e2e33' },
    { text: 'Blood Collector', value: '69e19e84-eeae-4361-bf8d-53456def3994' },
    { text: 'Donor Admin', value: 'c2f7694a-1d5e-4754-8575-2c62cbfdf627' },
    { text: 'Initial Screener', value: '9922d75f-0f7a-4fb2-ab1b-5f06b118136c' },
    { text: 'Inventory User', value: 'b42f4287-089a-41f9-89ef-a5a3f474f754' },
    { text: 'MedTech', value: 'c3f8818a-7a67-4d54-ba8e-3a5bd247e730' },
    { text: 'Physical Exam Screener', value: '22ee22cd-9580-4d5c-8e90-2d615283df1e' },
    { text: 'Requisition User', value: '84148d25-7e25-4bdc-bfb5-d0f3cd729ab7' }
  ];

const RequestTypeOptions: any = [
    { text: 'Routine', value: 'Routine'},
    { text: 'STAT', value: 'STAT'},
    { text: 'O.R. Use', value: 'ORUse'}
];

const CrossMatchingOptions: any = [
    { text: 'Saline Phase Only', value: 'SalinePhaseOnly'},
    { text: 'Saline Albumin Phase Only', value: 'SalineAlbuminPhaseOnly'},
    { text: 'Saline Albumin Globulin Phase Only', value: 'SalineAlbuminGlobulinPhaseOnly'}
];

const PosNegativeOptions: any = [
    { text: 'Positive', value: 'Positive'},
    { text: 'Negative', value: 'Negative'}
];

const CompatibilityOptions: any = [
    { text: 'Compatible', value: 'Compatible'},
    { text: 'Incompatible', value: 'Incompatible'},
    { text: 'Blank', value: 'Blank'},
    { text: 'Doubtful', value: 'Doubtful'}
];

const BloodTypeOptions: any = [
    { text: 'O', value: 'O'},
    { text: 'A', value: 'A'},
    { text: 'B', value: 'B'},
    { text: 'AB', value: 'AB'}
];

const RhOptions: any = [
    { text: 'Positive', value: 'Positive'},
    { text: 'Negative', value: 'Negative'},
    { text: 'Unknown', value: 'Unknown'}
];

const RequisitionStatusOptions: any = [
    { text: 'Received', value: 'Received'},
    { text: 'In Progress', value: 'In Progress'},
    { text: 'Cancelled', value: 'Cancelled'},
    { text: 'For Transfusion', value: 'For Transfusion'},
    { text: 'Done', value: 'Done' }
];

const TranfusionStatusOptions: any = [
    { text: 'Transfusion Completed', value: 'Completed'},
    { text: 'Transfusion Stopped', value: 'Stopped'}
];

const BloodTypingOptions: any = [
    { text: '0', value: '0'},
    { text: '1+', value: '1+'},
    { text: '2+', value: '2+'},
    { text: '3+', value: '3+'},
    { text: '4+', value: '4+'},
];

export default { 
    GenderOptions,
    RoleOptions,
    RequestTypeOptions,
    CrossMatchingOptions,
    PosNegativeOptions,
    CompatibilityOptions,
    BloodTypeOptions,
    RhOptions,
    RequisitionStatusOptions,
    TranfusionStatusOptions,
    BloodTypingOptions
};