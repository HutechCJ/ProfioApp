import React from 'react';
import Link from '@docusaurus/Link';

import SectionLayout from './SectionLayout';

import dotnet from '../../static/img/tech/dotnet.png';
import nextjs from '../../static/img/tech/nextjs.png';
import flutter from '../../static/img/tech/flutter.png';

const InvestorsList = [
  {
    url: 'https://dotnet.microsoft.com/en-us/',
    logo: dotnet,
  },
  {
    url: 'https://nextjs.org/',
    logo: nextjs,
  },
  {
    url: 'https://flutter.dev/',
    logo: flutter,
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
