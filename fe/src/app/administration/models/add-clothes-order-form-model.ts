export interface InsertClothesOrderModel {
    Id: number,
    Province: string,
    City: string,
    Area: string,
    Address: string,
    Price: number,
    Bed: number,
    Bath: number,
    Parking: number,
    PetFriendly: boolean,
    Garden: boolean,
    Pool: boolean,
    ErfSize: number,
    FloorSize: number,
    Description: string,
    PropertyType: string,
    ListingDate: string,
    OtherFeatures: string,
    RentSale: string,
    RatesTaxes: number,
    Levies: number,
    photos:{
        Id?: number,
            base64?: string
    }
}
