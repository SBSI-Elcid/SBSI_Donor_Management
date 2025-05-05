export class DownloadHelper {
    
    public static download(blobData: any, fileName: string): void {
        const blob = new Blob([blobData])
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = fileName;
        link.click();
        URL.revokeObjectURL(link.href);
    }
}