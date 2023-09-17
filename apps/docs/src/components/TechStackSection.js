import React from 'react';
import Link from '@docusaurus/Link';

import SectionLayout from './SectionLayout';

const TechList = [
  {
    url: 'https://dotnet.microsoft.com/en-us/',
    logo: require('../../static/img/tech/dotnet.png').default,
  },
  {
    url: 'https://nextjs.org/',
    logo: require('../../static/img/tech/nextjs.png').default,
  },
  {
    url: 'https://flutter.dev/',
    logo: require('../../static/img/tech/flutter.png').default,
  },
  {
    url: 'https://docusaurus.io/',
    logo: require('../../static/img/tech/docusaurus.png').default,
  },
];

const TechSection = () => {
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
        {TechList.map(({ url, logo }, idx) => (
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

export default TechSection;
