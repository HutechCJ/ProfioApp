export function getPagingQueryString(options: Partial<PagingOptions>): string {
    return `?${Object.entries(options)
        .map((value) => `${value[0]}=${value[1]}`)
        .join('&')}`
}
