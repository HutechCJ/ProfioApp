import React from 'react';
import clsx from 'clsx';
import styles from './styles.module.css';

type FeatureItem = {
  title: string;
  Svg: React.ComponentType<React.ComponentProps<'svg'>>;
  description: JSX.Element;
};

const FeatureList: FeatureItem[] = [
  {
    title: 'Truck Tracking',
    Svg: require('@site/static/img/tracking-track.svg').default,
    description: (
      <>
        Track your truck in real time. You can see the location of the truck on the map.
      </>
    ),
  },
  {
    title: 'Order Management',
    Svg: require('@site/static/img/order-mangement.svg').default,
    description: (
      <>
        The order management system allows you to manage orders. You can see the status of your order and the location of the truck.
      </>
    ),
  },
  {
    title: 'Incident Warning',
    Svg: require('@site/static/img/mobile-phone.svg').default,
    description: (
      <>
        The incident warning system allows administrator to warn the driver of an incident. The driver can see via mobile app.
      </>
    ),
  }
];

function Feature({ title, Svg, description }: FeatureItem) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
        <Svg className={styles.featureSvg} role="img" />
      </div>
      <div className="text--center padding-horiz--md">
        <h3>{title}</h3>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures(): JSX.Element {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
