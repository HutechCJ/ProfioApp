import MenuIcon from '@mui/icons-material/Menu'
import NotificationsIcon from '@mui/icons-material/Notifications'
import AppBar from '@mui/material/AppBar'
import Avatar from '@mui/material/Avatar'
import Grid from '@mui/material/Grid'
import IconButton from '@mui/material/IconButton'
import Tab from '@mui/material/Tab'
import Tabs from '@mui/material/Tabs'
import Toolbar from '@mui/material/Toolbar'
import Tooltip from '@mui/material/Tooltip'
import Typography from '@mui/material/Typography'
import * as React from 'react'
import Link from '../Link'

export const lightColor = 'rgba(255, 255, 255, 0.7)'

export interface HeaderProps {
    onDrawerToggle: () => void
    title?: string
}

export default function Header(props: HeaderProps) {
    const { onDrawerToggle, title } = props

    return (
        <React.Fragment>
            <HeaderDefault onDrawerToggle={onDrawerToggle} />
            <HeaderTitle title={title || 'Bảng Điều Khiển'} />
        </React.Fragment>
    )
}

export function HeaderDefault({ onDrawerToggle }: HeaderProps) {
    return (
        <AppBar color="primary" position="sticky" elevation={0}>
            <Toolbar>
                <Grid container spacing={1} alignItems="center">
                    <Grid sx={{ display: { sm: 'none', xs: 'block' } }} item>
                        <IconButton
                            color="inherit"
                            aria-label="open drawer"
                            onClick={onDrawerToggle}
                            edge="start"
                        >
                            <MenuIcon />
                        </IconButton>
                    </Grid>
                    <Grid item xs />
                    {/* <Grid item>
                        <Link
                            href="/"
                            variant="body2"
                            sx={{
                                textDecoration: 'none',
                                color: lightColor,
                                '&:hover': {
                                    color: 'common.white',
                                },
                            }}
                            rel="noopener noreferrer"
                            target="_blank"
                        >
                            Go to docs
                        </Link>
                    </Grid> */}
                    <Grid item>
                        <Tooltip title="Alerts • No alerts">
                            <IconButton color="inherit">
                                <NotificationsIcon />
                            </IconButton>
                        </Tooltip>
                    </Grid>
                    <Grid item>
                        <IconButton color="inherit" sx={{ p: 0.5 }}>
                            <Avatar
                                src="/static/images/avatar/1.jpg"
                                alt="My Avatar"
                            />
                        </IconButton>
                    </Grid>
                </Grid>
            </Toolbar>
        </AppBar>
    )
}

export function HeaderTitle({
    title,
    items,
}: {
    title: string
    items?: React.ReactNode[]
}) {
    return (
        <AppBar
            component="div"
            color="primary"
            position="static"
            elevation={0}
            sx={{ zIndex: 0 }}
        >
            <Toolbar>
                <Grid container alignItems="center" spacing={1}>
                    <Grid item xs>
                        <Typography color="inherit" variant="h5" component="h1">
                            {title}
                        </Typography>
                    </Grid>
                    {items &&
                        items.length &&
                        items.map((item, i) => {
                            return (
                                <Grid key={`header_grid_item_${i}`} item>
                                    {item}
                                </Grid>
                            )
                        })}
                </Grid>
            </Toolbar>
        </AppBar>
    )
}

export function HeaderTabs({
    items,
}: {
    items?: React.ReactNode[]
}) {
    return (
        <AppBar
            component="div"
            position="static"
            elevation={0}
            sx={{ zIndex: 0 }}
        >
            <Tabs value={0} textColor="inherit">
                <Tab label="Users" />
                <Tab label="Sign-in method" />
                <Tab label="Templates" />
                <Tab label="Usage" />
            </Tabs>
        </AppBar>
    )
}
