import React from 'react';
import Link from '@docusaurus/Link';

import SectionLayout from './SectionLayout';

const DeveloperCommunitySection = () => {
  return (
    <SectionLayout
      title="Join our developer community"
      description={`Open-source is in the ❤ of HutechCJ.\nFollow us ⭐ us on GitHub, and join our developer security community 🗣️ on Discord!`}
      style={{
        backgroundColor: '#1b1b1d',
        color: '#fff',
        padding: '50px 0',
      }}
    >
      <div
        style={{
          display: 'flex',
          justifyContent: 'center',
          gap: '10px',
          flexWrap: 'wrap',
        }}
      >
        <Link
          href="https://github.com/HutechCJ/ProfioApp"
          className="button button--primary button--outline"
        >
          Star on GitHub
        </Link>
        <Link
          href="https://discord.gg/3WFQGKqn"
          className="button button--primary button--outline"
        >
          Join Discord
        </Link>
      </div>
    </SectionLayout>
  );
};

export default DeveloperCommunitySection;
