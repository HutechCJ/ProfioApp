'use client'

import MediaCard from '../../components/MediaCard'
import Content from '../../components/paperbase/Content'

export default async function Index() {
    /*
     * Replace the elements below with your own.
     *
     * Note: The corresponding styles are in the ./index.styled-jsx file.
     */
    return (
        <div>
            Hello world
            <Content/>
            <MediaCard heading="Hello" text="world" />
        </div>
    )
}
