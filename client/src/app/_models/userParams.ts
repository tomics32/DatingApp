import { User } from "./user";

export class UserParams {
    gender: string;
    minAge = 18;
    maxAge = 99;
    pageNumber = 1;
    pageSize = 5;
    orderBy = 'lastActive';

    constructor(user: User | null){
        this.gender = user?.gender === 'female' ? 'male' : 'female';
        
        // Jeśli w localStorage znajdują się zapisane parametry, nadpisz domyślne wartości
        const savedParams = localStorage.getItem('userParams');
        if (savedParams) {
            const { minAge, maxAge, gender, orderBy, pageNumber, pageSize } = JSON.parse(savedParams);
            this.minAge = minAge || this.minAge;
            this.maxAge = maxAge || this.maxAge;
            this.gender = gender || this.gender;
            this.orderBy = orderBy || this.orderBy;
            this.pageNumber = pageNumber || this.pageNumber;
            this.pageSize = pageSize || this.pageSize;
        }
    }
}