import * as React from 'react';
import Box from '@mui/material/Box';
import Tab from '@mui/material/Tab';
import { TabContext, TabList, TabPanel } from '@mui/lab';

import LandingCharts from '../../assets/landing-charts.jpg';

export default function LabTabs() {
    const [value, setValue] = React.useState('1');

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValue(newValue);
    };

    return (
        <Box sx={{ width: '100%', typography: 'body1' }}>
            <TabContext value={value}>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <TabList onChange={handleChange}>
                        <Tab label="Wertvollste Kunden" value="1" />
                        <Tab label="Umsatz/Jahr" value="2" />
                        <Tab label="Zahlungen" value="3" />
                    </TabList>
                </Box>
                <TabPanel value="1" sx={{ p: 0}}>
                    <Box
                        component="img"
                        src={LandingCharts}
                        alt="Bald verfügbar"
                        sx={{ width: '100%', height: '100%', objectFit: 'cover' }}
                    />
                </TabPanel>
                <TabPanel value="2" sx={{ p: 0}}>
                    <Box
                        component="img"
                        src={LandingCharts}
                        alt="Bald verfügbar"
                        sx={{ width: '100%', height: '100%', objectFit: 'cover' }}
                    />
                </TabPanel>
                <TabPanel value="3" sx={{ p: 0}}>
                    <Box
                        component="img"
                        src={LandingCharts}
                        alt="Bald verfügbar"
                        sx={{ width: '100%', height: '100%', objectFit: 'cover' }}
                    />
                </TabPanel>
            </TabContext>
        </Box>
    );
}