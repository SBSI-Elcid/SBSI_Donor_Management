export interface IBBInventory {
    bagNo: string;                
    regNo: string;                
    bloodGroup: string;           
    component: string;            
    volume: string;            
    collectionDate?: string;       
    expiry: string;                
    source: string;               
    bGroup?: string;              
    rh?: string;                  
    bpStatusId?: string;          
    hasAllocation?: boolean;      
    firstnameAlloc?: string;      
    middlenameAlloc?: string;     
    lastnameAlloc?: string;       
    segmentSerialNo?: string;     
}

export class BBInventoryDTO implements IBBInventory {
    bagNo = '';
    regNo = '';
    bloodGroup = '';
    component = '';
    volume = '';
    collectionDate?: string;
    expiry = '';
    source = '';
    bGroup?: string;
    rh?: string;
    bpStatusId?: string;
    hasAllocation?: boolean;
    firstnameAlloc?: string;
    middlenameAlloc?: string;
    lastnameAlloc?: string;
    segmentSerialNo?: string;
}
