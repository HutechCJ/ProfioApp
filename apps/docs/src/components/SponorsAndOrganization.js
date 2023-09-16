import React from 'react';
import Link from '@docusaurus/Link';

import SectionLayout from './SectionLayout';

const SponorList = [
  {
    url: 'https://hutech.edu.vn/',
    logo: require('../../static/img/organization/hutech.png').default,
  },
  {
    url: 'https://cjvietnam.vn/',
    logo: require('../../static/img/organization/cj.png').default,
  },
];

const SponorSection = () => {
  return (
    <SectionLayout
      title="Sponors and Organization"
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
        {SponorList.map(({ url, logo }, idx) => (
          <div className="col col--4" key={idx}>
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
                  <img loading="lazy" src={logo} />
                </Link>
              </div>
            </div>
          </div>
        ))}
      </div>
    </SectionLayout>
  );
};

export default SponorSection;
