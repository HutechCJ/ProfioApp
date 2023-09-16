import React from 'react';
import Link from '@docusaurus/Link';

import SectionLayout from './SectionLayout';

const InvestorsList = [
  {
    url: 'https://dotnet.microsoft.com/en-us/',
    logo: 'https://i.imgur.com/HYadxRR.png',
  },
  {
    url: 'https://nextjs.org/',
    logo: 'https://i.imgur.com/U9hrfQp.png',
  },
  {
    url: 'https://flutter.dev/',
    logo: 'https://i.imgur.com/NYNWmfc.png',
  },
];

const InvestorsSection = () => {
  return (
    <SectionLayout
      title="Tentative Technologies"
      style={{ backgroundColor: 'white' }}
      titleStyle={{ color: '#444950' }}
    >
      <div
        className="row"
        style={{
          justifyContent: 'center',
          gap: '5px',
        }}
      >
        {InvestorsList.map(({ url, logo }, idx) => (
          <div className="col col--2" key={idx}>
            <div className="col-demo text--center">
              <div
                style={{
                  minHeight: '70px',
                  alignItems: 'center',
                  display: 'flex',
                  justifyContent: 'center',
                }}
              >
                <Link href={url}>
                  <img loading="lazy" src={logo} style={{ width: '100px' }} />
                </Link>
              </div>
            </div>
          </div>
        ))}
      </div>
    </SectionLayout>
  );
};

export default InvestorsSection;
