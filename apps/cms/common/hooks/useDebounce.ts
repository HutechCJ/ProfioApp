import { useEffect, useMemo, useRef, useState } from 'react'

export function useDebouncedState<T = any>(
    defaultValue: T,
    wait: number | undefined = 300,
    options = { leading: false }
) {
    const [value, setValue] = useState<T>(defaultValue)
    const [loading, setLoading] = useState(false)

    // leading : update value with first call
    const leadingRef = useRef(true)
    const timeoutRef = useRef<number | null>(null)

    const clearTimeout = () => window.clearTimeout(timeoutRef.current as any)

    // clear timeout when call
    useEffect(() => clearTimeout, [])

    const debouncedSetValue = (newValue: T) => {
        clearTimeout()
        if (leadingRef.current && options.leading) {
            setValue(newValue)
        } else {
            setLoading(true)
            timeoutRef.current = window.setTimeout(() => {
                leadingRef.current = true
                setValue(newValue)
                setLoading(false)
            }, wait)
        }
        leadingRef.current = false
    }

    const state = useMemo(
        () => ({
            value,
            loading,
        }),
        [value, loading]
    )

    const handlers = useMemo(
        () => ({
            debouncedSetValue,
        }),
        []
    )

    return [state, handlers] as const
}
