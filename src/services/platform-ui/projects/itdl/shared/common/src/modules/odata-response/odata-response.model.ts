export class ODataResponse<T> {
    public value!: T;
    public '@odata.count': number;
    public '@odata.nextLink': string;
    public '@odata.context': string;
}
