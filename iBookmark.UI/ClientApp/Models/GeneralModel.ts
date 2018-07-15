export interface TextBoxGroupProps {
    name: string,
    value: string,
    label: string,
    error?: string,
    type?: string,
    onChange: (event: React.ChangeEvent<HTMLInputElement>) => void
}