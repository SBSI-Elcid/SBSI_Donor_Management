export default class Common {
    public static hasValue(value: any): boolean {
        return (typeof (value) !== 'undefined' && value !== null && value !== '');
    }

    public static isNull(value: any): boolean {
        return (typeof (value) === 'undefined' || value === null);
    }

    public static isArray(value: any, checkIfArrayIsEmpty: boolean = false): boolean {
        if (typeof (value) !== 'undefined' && value !== null && Object.prototype.toString.call(value) === '[object Array]') {
            return checkIfArrayIsEmpty ? (value.length > 0) : true;
        } else {
            return false;
        }
    }

    public static startDownload(blobUrl: string, fileName: string): void {
        let a: HTMLAnchorElement = document.createElement('a');
        a.download = fileName;
        a.href = blobUrl;
        document.body.appendChild(a);
        a.click();
        a.remove();
      }

    public static ValidationRules: {
        required: (value: any) => boolean | string,
        maxLength: (maxLength: number) => (value: any) => boolean | string,
        emailFormat: (values: any) => boolean | string,
        arrayRequired: (value : any[]) => boolean | string,
        minValue: (minValue: number) => (value: any) => boolean | string
    } = {
        required: v =>  ((typeof(v) == "number") ? !!(v.toString()) : !!v) || "Field is required.",
        maxLength: (maxLength: number = 0) => v => (!Common.hasValue(v) || (!!v && v.toString().length <= maxLength)) || `Maximum length is ${maxLength} characters.`,
        emailFormat: (value: string) => !value || (!!value && Common.emailFormat.test(value)) || 'E-mail must be valid.',
        arrayRequired: (value) => (!!value && value.length > 0) || "At least one selection is required.",
        minValue: (minValue: number = 0) => v => v >= minValue || `Minimum value is ${minValue}.`
    };
    
    // Regular expression for email format used in Savanta
    public static emailFormat: RegExp = /^.+@.+\..+$/;

}