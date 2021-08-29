import { BaseModel } from '../../base-model';

export class Paginacao<T extends BaseModel> {
    content: T[];
    first: boolean;
    last: boolean;
    number: number;
    numberOfElements: number;
    size: number;
    sort: string;
    totalElements: number;
    totalPages: number;
}
