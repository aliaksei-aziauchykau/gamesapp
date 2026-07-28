export class ODataQuery {
    constructor(public readonly query: string) {}

    public static create(query: string): ODataQuery {
        return new ODataQuery(query);
    }
}
